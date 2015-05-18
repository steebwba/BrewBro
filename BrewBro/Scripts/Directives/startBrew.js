//This directive will only work if added as an attribute (e.g. <button start-brew groupid="idForGroup" />)
//It will display a modal popup to initiate a brew for a given group
(function () {
    var app = angular.module('directivesModule', []);

    app.directive('startBrew', function ($compile) {

        var controller = function ($scope, $modal) {
            $scope.brew = function () {
                $modal.open({
                    templateUrl: 'Home/StartBrew',
                    controller: 'brewController',
                    animation: true,
                    resolve: {
                        GroupId: function () {
                            return $scope.groupId;
                        }
                    }
                });
            };
        }

        var loadTemplate = function ($scope, $element, $attrs) {
            $attrs.$observe('group-id', function (value) {
                $scope.groupId = value;
            });

            var cssClass = 'btn';
            if (typeof ($scope.styling) != 'undefined') {
                cssClass += $scope.styling;
            }
            else {
                cssClass += 'btn-default';
            }

            $element.on('click', $scope.brew);

            $element[0].innerHTML = ($scope.showText) ? '<i class="fa fa-coffee"></i>&nbsp;Start Brew!' : '<i class="fa fa-coffee"></i>';


            $element.addClass('btn');

            if (typeof ($scope.styling) != 'undefined') {
                $element.addClass($scope.styling);
            }
            else {
                $element.addClass('btn-default');
            }
        }

        return {
            restrict: 'A',
            controller : controller,
            scope: {
                groupId: '@',
                showText: '=',
                styling: '@styling',
            },
            link: loadTemplate,
            require: '?groupid'
        };
    });

}());