projectServices = angular.module('brewService', ['ngResource']);


projectServices.factory('BrewService',
  function ($resource) {
      return $resource('api/Brew/', {}, {

          save: { method: 'POST', url: 'api/Group/:groupId/Brew/:userId', params: { groupId: '@groupId', userId: '@userId' } },
          query: { method: 'GET', url: 'api/Group/:groupId/Brew/', params: { groupId: '@groupId' }, isArray : true }

      });
  });
