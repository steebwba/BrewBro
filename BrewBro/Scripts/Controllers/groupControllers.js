var groupControllers = angular.module('groupControllers', []);

groupControllers.controller('groupListCtrl',
  function ($scope, $http, Group, $location) {
      $http.get('/api/Group/LoadAllGroups').
      success(function (data, status, headers, config) {
          $scope.groups = data;
              
          }).
      error(function (data, status, headers, config) {
              alert("error");
      });
  });

groupControllers.controller('groupCreateController', ['$scope', 'Group', '$location',
  function ($scope, Group, $location) {
      $scope.Model = undefined;
      $scope.showNewProjectForm = function () {
          $scope.Model = {};
      }
  }]);