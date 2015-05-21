var groupControllers = angular.module('groupControllers', []);

groupControllers.controller('groupHomeController',
  function ($scope, $http, $location, GroupService, Auth, Group) {
      $scope.SearchParameters = {
          GroupName: ''
      }

      $scope.groups = [];

      $scope.loadGroups = function () {
          GroupService.getByUser({ searchText: $scope.SearchParameters.GroupName, userId: Auth.getUser().Id }, function (data) {
              $scope.groups = data;
          }, function () {
              alert("error");
          });
      }

      $scope.showNewGroupForm = function () {
          $location.path('/Groups/Add');
      }

      $scope.viewGroup = function (id) {
          $location.path('/Groups/' + id);
      }

      $scope.isGroupOwner = function (ownerId) {
          return Group.isUserGroupOwner(ownerId);
      }


      $scope.loadGroups();
  });

groupControllers.controller('groupViewController',
  function ($scope, $http, $location, $routeParams, GroupService, BrewService, Auth, Group) {
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

      $scope.BrewHistory = {};

      $scope.addMode = ($routeParams.id == 'Add');
      $scope.editMode = false;
      $scope.usersToSelect = [];
      $scope.startEdit = function () {
          $scope.editMode = true;
          //clone the object so we can get back to it if we cancel
          $scope.GroupEdit.Group = JSON.parse(JSON.stringify($scope.Group));
      }
      $scope.isGroupOwner = function () {
          return Group.isUserGroupOwner($scope.Group.Owner.Id);

      }
      $scope.isLoggedInUser = function (userId) {
          return userId == Auth.getUser().Id;

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

          if (typeof ($scope.GroupEdit.Group.Users) != 'undefined') {
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
              //No owner, so make the user triggering the save the owner
              if ($scope.GroupEdit.Group.Owner == null) {
                  $scope.GroupEdit.Group.Owner = { Id: Auth.getUser().Id }
              }

              GroupService.save($scope.GroupEdit.Group, function (data) {
                  if ($scope.addMode) {
                      var ownerId = Auth.getUser().Id;

                      $scope.GroupEdit.Group.Owner = {
                          Id: ownerId
                      };

                      $location.path('/Groups/' + data.Id);
                  }
                  else {
                      //clone object to prevent undesired modifications by reference
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
          var userIndexToRemove = $scope.GroupEdit.Group.Users.map(function (el) { return el.Id; }).indexOf(id);
          $scope.GroupEdit.Group.Users.splice(userIndexToRemove, 1);
      }

      if ($scope.addMode) {
          $scope.startEdit();
          var user = Auth.getUser();
          $scope.GroupEdit.Group.Users = [{ Id: user.Id, Name: user.Name }];
      }
      else {
          GroupService.get({ id: $routeParams.id }, function (data) {
              $scope.Group = data;
              $scope.BrewHistory = BrewService.query({ groupId: $scope.Group.Id })
          }, function () {

          });
      }


  });