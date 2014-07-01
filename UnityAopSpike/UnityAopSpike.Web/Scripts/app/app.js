(function() {
    'use strict';
    
    var app = angular.module('UnityAop', [
        'ngCookies',
        'ngResource',
        'ngSanitize',
        'ngRoute',
        'ngAnimate',
        'ui.bootstrap'
    ]);

    app.config(function($routeProvider, $locationProvider) {
        $routeProvider.when('/products', {
            templateUrl: 'Partials/product-listing.html',
            controller: 'ProductsController'
        });

        $routeProvider.otherwise({ redirectTo: '/' });

        return $locationProvider.html5Mode(true);
    });

}).call(this);
