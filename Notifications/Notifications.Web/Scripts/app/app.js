var smsApp = angular.module('smsApp', ['ui.router']);

angular.module('plunker', [
    'ui.router'
])
  .config(function ($stateProvider, $urlRouterProvider) {
      $urlRouterProvider.otherwise("/");
      //
      // Now set up the states
      $stateProvider
        .state('orders', {
            url: "/orders/index.html",
            template: "<orders></orders>"
        })
        .state('company', {
            url: "/company/index.html",
            template: "<about></about>"
        })
      .state('customers', {
          url: "/customers/index.html",
          template: "<customers></customers>"
      })
  }
  );