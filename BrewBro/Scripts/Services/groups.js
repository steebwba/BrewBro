var projectServices = angular.module('groupServices', ['ngResource']);


projectServices.factory('GroupService',
  function ($resource) {
      return $resource('api/Group/:id');
  });
