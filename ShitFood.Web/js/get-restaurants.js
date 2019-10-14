function getRestaurants(lat, lng) {
  // var uri =
  //   "http://localhost:7071/api/getshit?lat=" +
  //   lat +
  //   "&lng=" +
  //   lng;
  var uri =
    "https://shitfoodapi.azurewebsites.net/api/getshit?lat=" +
    lat +
    "&lng=" +
    lng;
  axios
    .get(uri)
    .then(function(response) {
      for (var i = 0; i < response.data.length; i++) {
        L.marker([response.data[i].lat, response.data[i].lng], {
          icon: cutleryIconColour(response.data[i].foodHygieneRating),
          alt: response.data[i].name,
          title: response.data[i].name
        })
          .addTo(mymap)
          .bindPopup(
            '<strong>' + response.data[i].name + '</strong><br>Food Hygiene Rating: ' + '<strong>' + response.data[i].foodHygieneRating + '</strong> (' + hygieneRatingPhrase(response.data[i].foodHygieneRating) + ')'
          );
      }
      console.log(response.data);
    })
    .catch(function(error) {
      console.log(error);
    });
}

function cutleryIconColour(rating) {
  if (rating === "2") {
    return L.icon({
      iconUrl: "./img/markers/cutlery-yellow.svg",
      iconSize: [36, 36],
      iconAnchor: [10, 10],
      popupAnchor: [34, 0]
    });
  } else if (rating === "1") {
    return L.icon({
      iconUrl: "./img/markers/cutlery-orange.svg",
      iconSize: [36, 36],
      iconAnchor: [10, 10],
      popupAnchor: [34, 0]
    });
  } else if (rating === "0") {
    return L.icon({
      iconUrl: "./img/markers/cutlery-red.svg",
      iconSize: [36, 36],
      iconAnchor: [10, 10],
      popupAnchor: [34, 0]
    });
  }
  return L.icon({
    iconUrl: "./img/markers/cutlery.svg",
    iconSize: [36, 36],
    iconAnchor: [10, 10],
    popupAnchor: [34, 0]
  });
}

function hygieneRatingPhrase(rating) {
  switch (rating) {
    case "2":
      return "Improvement Necessary";
    case "1":
      return "Major Improvement Necessary";
    case "0":
      return "Urgent Improvement Necessary";
  }
}
