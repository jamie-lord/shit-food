var axios = require('axios');

function getRestaurants() {
  axios.get('https://shitfoodapi.azurewebsites.net/api/getshit?lat=52.9548&lng=-1.1581')
      .then(function (response) {
        for (var i = 0; i < response.length; i++) {
          L.marker(
              [
                response[i].lat,
                response[i].lng
              ],
              {
                icon: myIcon,
                alt: response[i].name,
                title: response[i].name,
              }
          ).addTo(mymap)
              .bindPopup(response[i].name + ' : Food Hygiene Rating - ' + response[i].foodHygieneRating);
        }
        console.log(response);
      })
      .catch(function (error) {
        console.log(error);
      })
      .finally(function () {
        alert('Enjoy your food')
      });
}
