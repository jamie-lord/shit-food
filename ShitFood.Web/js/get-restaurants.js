var axios = require('axios');

function getRestaurants() {
  var cutleryIcon = L.icon({
    iconUrl: './img/markers/cutlery.svg',
    iconSize: [36, 36],
    iconAnchor: [0, 0],
    popupAnchor: [18, -3]
  });

  var cutleryIconYellow = L.icon({
    iconUrl: './img/markers/cutlery-yellow.svg',
    iconSize: [36, 36],
    iconAnchor: [0, 0],
    popupAnchor: [18, -3]
  });

  var cutleryIconOrange = L.icon({
    iconUrl: './img/markers/cutlery-orange.svg',
    iconSize: [36, 36],
    iconAnchor: [0, 0],
    popupAnchor: [18, -3]
  });

  var cutleryIconRed = L.icon({
    iconUrl: './img/markers/cutlery-red.svg',
    iconSize: [36, 36],
    iconAnchor: [0, 0],
    popupAnchor: [18, -3]
  });

  axios.get('https://shitfoodapi.azurewebsites.net/api/getshit?lat=52.9548&lng=-1.1581')
      .then(function (response) {
        for (var i = 0; i < response.data.length; i++) {
          L.marker(
              [
                response.data[i].lat,
                response.data[i].lng
              ],
              {
                icon: cutleryIconColour(response.data[i].foodHygieneRating),
                alt: response.data[i].name,
                title: response.data[i].name,
              }
          ).addTo(mymap)
              .bindPopup(response.data[i].name + ' : Food Hygiene Rating - ' + response.data[i].foodHygieneRating);
        }
        console.log(response.data);
      })
      .catch(function (error) {
        console.log(error);
      })
      .finally(function () {
        alert('Enjoy your food')
      });
}

function cutleryIconColour(rating) {
  if (rating === 2) {
    return cutleryIconYellow
  }
  else if (rating === 1) {
    return cutleryIconOrange
  }
  else if (rating === 0) {
    return cutleryIconRed
  }
  else {
    return cutleryIcon
  }
}
