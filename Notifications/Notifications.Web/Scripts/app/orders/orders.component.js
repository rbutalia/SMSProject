
angular
  .module('smsApp')
  .component('ordersList', {
      templateUrl: '/orders/index.html',
          

      controller: function ordersController($scope) {
          $scope.orders = [
              {
                  oID: 1,
                  oDate: '24/02/2017'
              },
              {
                  oID: 2,
                  oDate: '24/04/2017'
              },
              {
                  oID: 3,
                  oDate: '28/02/2017'
              }
          ];
      }
  });