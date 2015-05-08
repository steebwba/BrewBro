var groupControllers = angular.module('groupControllers', []);

groupControllers.controller('groupListCtrl',
  function ($scope, $http, Group, $location) {

      $scope.loadGroups = function () {
          $http.get('/api/Group/LoadAllGroups').
              success(function (data, status, headers, config) {
                  $scope.groups = data;

              }).
              error(function (data, status, headers, config) {
                  alert("error");
              });
      }

      $scope.loadGroups();

      var groupAddedEvent = $scope.$on('groupAdded', function () {
          alert('OMG!');
          $scope.loadGroups();
      });
  });

groupControllers.controller('groupCreateController', ['$scope', 'Group', '$location',
  function ($scope, Group, $location) {
      $scope.editMode = false;
      $scope.Name = '';
      $scope.Users = [];
      $scope.showNewGroupForm = function () {
          $scope.editMode = true;
      }
      $scope.closeNewGroupForm = function () {
          $scope.editMode = false;
      }
      $scope.searchUsers = function () {
          $scope.Users.push({

              Name: "Test User",
              Id: 1,
              Selected: false
          }),
          $scope.Users.push({

              Name: "Test User 2",
              Id: 2,
              Selected: false
          })
      }
      $scope.save = function () {
          $scope.editMode = false;
          $scope.$emit('groupAdded');
      }

  }]);