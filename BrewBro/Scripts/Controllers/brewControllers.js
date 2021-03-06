﻿var brewControllers = angular.module('brewControllers', []);

brewControllers.controller('brewController',
  function ($scope, $http, $location, $modalInstance, GroupId, GroupService, BrewService, Auth) {
      $scope.Brew = {
          GroupId: GroupId,
          SelectedUser: null
      }
      $scope.Group = {};
      $scope.Brewing = false;
      $scope.brewEvent;
      $scope.brewCountdownEvent;
      $scope.Countdown = 5;

      //TODO Move to angular service
      var countDown = function () {
          setTimeout(function () {
              $scope.Countdown--;
              if ($scope.Countdown == 0) {
                  //TODO put to webservice so that weighting can be used and return selected user
                  //TODO save brew history
                  clearInterval($scope.brewEvent);
                  $scope.brewEvent = null;
                  $scope.$apply();
                  BrewService.save({ groupId: $scope.Group.Id, userId: $scope.Brew.SelectedUser.Id }, function () {
                        //TODO broadcast brew creation
                  },
                  function () {
                      alert('oh no!');
                  });


              }
              else {
                  countDown();
              }
          }, 1000);
      }

      $scope.doBrew = function () {
          //TODO Move to angular service
          //get selected users
          var users = $scope.Group.Users.filter(function (el) { return el.Selected; });
          if (users.length <= 1) {
              //TODO toast message or message in popup
              alert('Select more than 1 person, bro!');
          }
          else {
              $scope.Brewing = true;
              $scope.brewEvent = setInterval(function () {
                  //Object.getOwnPropertyNames seems to throw an error here, so had to fudge it, in a way, to determine if a user has already been selected
                  var availableUsers = (typeof ($scope.Brew.SelectedUser) == 'undefined' || $scope.Brew.SelectedUser == null) ? users : users.filter(function (el) { return el.Id != $scope.Brew.SelectedUser.Id; });

                  $scope.Brew.SelectedUser = availableUsers[Math.floor(Math.random() * availableUsers.length)];

                  $scope.$apply();
              }, 150);

              $scope.brewCountdownEvent = countDown();
          }


      }

      $scope.getUsers = function () {
          var user = Auth.getUser();
          if (typeof ($scope.Group.Users) != 'undefined') {
              return $scope.Group.Users.filter(function (el) { return user.Id != el.Id; })
          }
          else {
              return [];
          }
      }

      GroupService.get({ id: GroupId }, function (data) {
          $scope.Group = data;

          $scope.Group.Users.forEach(function (el) { el.Selected = true; });
      });



      $scope.cancel = function () {
          $modalInstance.dismiss('cancel');
          clearInterval($scope.brewEvent);
      };

  });

