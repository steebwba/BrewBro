var groupControllers = angular.module('groupControllers', []);

groupControllers.controller('groupHomeController',
  function ($scope, $http, $location, $modal) {
      $scope.SearchParameters = {
          GroupName: ''
      }

      $scope.groups = [];

      $scope.loadGroups = function () {
          $http.get('/api/Group?searchText=' + $scope.SearchParameters.GroupName).
              success(function (data, status, headers, config) {
                  $scope.groups = data;
              }).
              error(function (data, status, headers, config) {
                  alert("error");
              });
      }

      $scope.showNewGroupForm = function () {
          $modal.open({
              templateUrl: 'Home/NewGroup',
              controller: 'groupCreateController',
              animation: true
          });
      }

      $scope.viewGroup = function (id) {
          $location.path('/Groups/' + id);
      }

      $scope.loadGroups();

      var groupAddedEvent = $scope.$on('groupAdded', function () {
          $scope.loadGroups();
      });
  });

groupControllers.controller('groupViewController',
  function ($scope, $http, $location, $modal, $routeParams, GroupService) {
      $scope.Group = {}
      GroupService.get({ id: $routeParams.id }, function (data) {
          $scope.Group = data;
      });

  });
groupControllers.controller('groupCreateController',
  function ($scope, $http, $location, GroupService, $modalInstance) {
      $scope.editMode = false;
      $scope.Group = {
          Name: '',
          Users: [],
          UsersToSelect: [],
          SearchText: ''
      }

      $scope.searchUsers = function () {
          $http.get('/api/User?searchText=' + $scope.Group.SearchText)
               .success(function (data) {
                   var selectedItems = $scope.Group.UsersToSelect.filter(function (el) { return el.Selected });
                   var len = data.length;
                   $scope.Group.UsersToSelect = selectedItems;

                   for (var i = 0; i < len; i++) {
                       //check to see if the item is already a selected item
                       if ($scope.Group.UsersToSelect.filter(function (el) { return el.Id == data[i].Id }).length == 0) {
                           $scope.Group.UsersToSelect.push(data[i]);
                       }
                   }
               })
               .error(function () {
                   //TODO error dialog/toast
                   alert('it went wrong!');
               });
      }
      $scope.save = function () {
          //Map the user id's to the users in the group, so that that data stored for the user against a group is minimized
          //When listing the users in a group during edit/view, the service layer can flesh out the objects
          $scope.Group.Users = $scope.Group.UsersToSelect.filter(function (el) { return el.Selected }).map(function (el) { return { Id: el.Id } });

          GroupService.save($scope.Group, function () {
              $scope.$emit('groupAdded');
              $scope.editMode = false;
              alert('yay!');
          }, function () {
              alert('error!');
          });


      }
      $scope.cancel = function () {
          $modalInstance.dismiss('cancel');
      };

  });