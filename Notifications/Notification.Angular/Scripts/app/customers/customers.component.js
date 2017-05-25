
angular
  .module('smsApp')
  .component('customersList', {
      templateUrl: '/customers/index.html',


      controller: function ordersController($scope) {
          $scope.customers = [
              {
                  cID: 1,
                 cName: 'Randeep Butalia'
              },
              {
                  cID: 2,
                  cName: 'Manit Butalia'
              },
              {
                  cID: 3,
                  cName: 'Manya Butalia'
              }
          ];
      }
  });