var projectServices = angular.module('userService', ['ngResource']);


projectServices.factory('UserService',
  function ($resource) {
      return $resource('api/User/:id');
  });
