var groupControllers = angular.module('groupControllers', []);

groupControllers.controller('groupViewController',
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

groupControllers.controller('groupCreateController',
  function ($scope, $http, $location) {
      $scope.editMode = false;
      $scope.Group = {
          Name: '',
          Users: [],
          SearchText: ''
      }
      $scope.showNewGroupForm = function () {
          $scope.editMode = true;
      }
      $scope.closeNewGroupForm = function () {
          $scope.editMode = false;
      }
      $scope.searchUsers = function () {
          $http.get('/api/User?searchText=' + $scope.Group.SearchText)
               .success(function (data) {
                   alert(JSON.stringify(data));
                   var selectedItems = $scope.Group.Users.filter(function (el) { return el.Selected });
                   var len = data.length;
                   $scope.Group.Users = selectedItems;

                   for (var i = 0; i < len; i++) {
                       //check to see if the item is already a selected item
                       if ($scope.Group.Users.filter(function (el) { return el.Id == data[i].Id }).length == 0)
                       {
                           $scope.Group.Users.push(data[i]);
                       }
                   }
               })
               .error(function () {
                   //TODO error dialog/toast
                   alert('it went wrong!');
               });
      }
      $scope.save = function () {
          $scope.editMode = false;
          $scope.$emit('groupAdded');
      }

  });