
angular
  .module('smsApp')
  .component('company', {
      templateUrl: '/company/index.html',


      controller: function ordersController($scope) {
          $scope.company = 
              {
                  CompanyName: "DoughZone",
                  ContactName: "Edwin Zelwegger",
                  Telephone: "+16473432321",
                  TaxRate: "6.90"
              };
      }
  });