var axios = require('axios');

function getRestaurants() {
  var myIcon = L.icon({
    iconUrl: '../img/markers/cutlery.svg',
    iconSize: [36, 36], // size of the icon
    iconAnchor: [0, 0], // point of the icon which will correspond to marker's location
    popupAnchor: [18, -3] // point from which the popup should open relative to the iconAnchor
  });

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
