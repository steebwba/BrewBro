var projectServices = angular.module('groupServices', ['ngResource']);


projectServices.factory('Group', ['$resource',
  function ($resource) {
      return $resource('/api/Group/', {}, {
          getAll: { method: 'GET', isArray: true }
      });
  }]);
