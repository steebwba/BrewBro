projectServices = angular.module('brewService', ['ngResource']);


projectServices.factory('BrewService',
  function ($resource) {
      return $resource('api/Brew/:id');
  });
