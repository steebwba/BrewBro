var projectServices = angular.module('groupService', ['ngResource']);

projectServices.factory('GroupService',
  function ($resource) {
      return $resource('api/Group/:id', { id: '@id' },
            {
                getByUser: { method: 'GET', url: 'api/User/:userId/Group/:searchText', isArray: true, params: { userId: '@userId', searchText: '@searchText' } },
                removeUser: { method: 'DELETE', url: 'api/Group/:groupId/User/:userId', params: { groupId: '@groupId', userId: '@userId' }}
            }
          );
  });