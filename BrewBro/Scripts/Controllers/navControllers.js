﻿var navControllers = angular.module('navControllers', []);

navControllers.controller('navController',
  function ($scope, $http, $location, Auth) {
     
      $scope.User = Auth.getUser();

      //Calls authentication service method to check if user is logged in
      $scope.userLoggedIn = Auth.isLoggedIn;
  }
  );