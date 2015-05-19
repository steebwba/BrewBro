projectServices = angular.module('brewServices', ['ngResource']);


projectServices.factory('BrewService',
  function ($resource) {
      return $resource('api/Brew/:id');
  });
