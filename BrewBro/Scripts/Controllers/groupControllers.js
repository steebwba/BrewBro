var groupControllers = angular.module('groupControllers', []);

groupControllers.controller('groupHomeController',
  function ($scope, $http, $location) {
      $scope.SearchParameters = {
          GroupName: ''
      }

      $scope.groups = [];

      $scope.loadGroups = function () {
          $http.get('api/Group?searchText=' + $scope.SearchParameters.GroupName).
              success(function (data, status, headers, config) {
                  $scope.groups = data;
              }).
              error(function (data, status, headers, config) {
                  alert("error");
              });
      }

      $scope.showNewGroupForm = function () {
          $location.path('/Groups/Add');
      }

      $scope.viewGroup = function (id) {
          $location.path('/Groups/' + id);
      }

      $scope.loadGroups();
  });

groupControllers.controller('groupViewController',
  function ($scope, $http, $location, $routeParams, GroupService) {

      function resetEdit() {
          $scope.editMode = false;
          $scope.GroupEdit = {
              SearchText: '',
              Group: {},
              UsersToSelect: []
          };
      }

      $scope.Group = {};
      $scope.GroupEdit = {
          SearchText: '',
          Group: {},
          UsersToSelect: []
      };
      $scope.addMode = ($routeParams.id == 'Add');
      $scope.editMode = false;
      $scope.usersToSelect = [];
      $scope.startEdit = function () {
          $scope.editMode = true;
          //clone the object so we can get back to it if we cancel
          $scope.GroupEdit.Group = JSON.parse(JSON.stringify($scope.Group));
      }

      $scope.getUserList = function () {

          if ($scope.editMode) {
              return $scope.GroupEdit.Group.Users;
          }
          else {
              return $scope.Group.Users;
          }
      }

      $scope.searchUsers = function () {
          $scope.GroupEdit.UsersToSelect = [];

          $http.get('api/User?searchText=' + $scope.GroupEdit.SearchText)
               .success(function (data) {
                   var len = data.length;

                   for (var i = 0; i < len; i++) {
                       //check to see if the item is already a selected item
                       if (typeof ($scope.GroupEdit.Group.Users) != 'undefined') {
                           if ($scope.GroupEdit.Group.Users.filter(function (el) { return el.Id == data[i].Id }).length == 0) {
                               $scope.GroupEdit.UsersToSelect.push(data[i]);
                           }
                       }
                       else {
                           $scope.GroupEdit.UsersToSelect = data;
                       }
                   }

                   $scope.GroupEdit.SearchText = '';

               })
               .error(function () {
                   //TODO error dialog/toast
                   alert('it went wrong!');
               });
      }

      $scope.addUser = function (id) {
          var userToAdd = $scope.GroupEdit.UsersToSelect.filter(function (el) { return el.Id == id })[0];

          if (typeof ($scope.GroupEdit.Group.Users) != 'undefined')
          {
              $scope.GroupEdit.Group.Users.push(userToAdd);
             
          }
          else {
              $scope.GroupEdit.Group.Users = [userToAdd];
          }
          
          $scope.GroupEdit.UsersToSelect.splice(userToAdd);
      }

      $scope.cancelEdit = function () {
          resetEdit();
      }

      $scope.save = function () {

          $scope.$broadcast('show-errors-check-validity');

          if ($scope.frmGroup.$valid) {
              GroupService.save($scope.GroupEdit.Group, function (data) {
                  if ($scope.addMode) {
                      $location.path('/Groups/' + data.Id);
                  }
                  else {
                      $scope.Group = JSON.parse(JSON.stringify($scope.GroupEdit.Group));
                      resetEdit();
                  }
              }, function () {
                  alert('error!');
              });
          }
      }



      $scope.kickUser = function (id) {
          alert('kick ' + id);
      }


      $scope.removeUser = function (id) {
          var userToRemove = $scope.GroupEdit.Group.Users.filter(function (el) { el.Id == id; });

          $scope.GroupEdit.Group.Users.splice(userToRemove);
      }

      if ($scope.addMode) {
          $scope.startEdit();
      } else {
          GroupService.get({ id: $routeParams.id }, function (data) {
              $scope.Group = data;
          }, function () {

          });
      }


  });