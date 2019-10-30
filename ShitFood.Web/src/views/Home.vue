<template>
  <div class="home">
    <l-map
      style="width: 100vw; height: 100vh"
      :center="myPosition"
      :zoom="zoom"
    >
      <l-tile-layer
        :url="'https://{s}.tile.openstreetmap.se/hydda/full/{z}/{x}/{y}.png'"
      ></l-tile-layer>
      <l-marker
        :latLng="myPosition"
        :icon="myIcon"
        alt="Current marker"
        title="This is you"
      ></l-marker>
      <l-marker
        v-for="(place, index) in places"
        :icon="cutleryIcon"
        :key="index"
        :latLng="getPlacePosition(place)"
      ></l-marker>
    </l-map>
  </div>
</template>

<script lang="ts">
import {
  Component,
  Emit,
  Mixins,
  Model,
  Prop,
  Vue
} from "vue-property-decorator";
import L from "leaflet";
import { LMap, LTileLayer, LMarker } from "vue2-leaflet";
import Axios from "axios";

@Component({
  components: { LMap, LTileLayer, LMarker }
})
export default class Home extends Vue {
  private myPosition: L.LatLng = new L.LatLng(0, 0);
  private zoom = 4;
  private places: object[] = [];
  private myIcon: L.Icon = new L.Icon({
    iconUrl: "img/markers/me.svg",
    iconSize: [36, 36], // size of the icon
    iconAnchor: [18, 18], // point of the icon which will correspond to marker's location
    popupAnchor: [34, 0] // point from which the popup should open relative to the iconAnchor
  });
  private cutleryIcon: L.Icon = new L.Icon({
    iconUrl: "img/markers/cutlery.svg",
    iconSize: [36, 36], // size of the icon
    iconAnchor: [18, 18], // point of the icon which will correspond to marker's location
    popupAnchor: [34, 0] // point from which the popup should open relative to the iconAnchor
  });

  private getPosition() {
    navigator.geolocation.getCurrentPosition(position => {
      this.myPosition.lat = position.coords.latitude;
      this.myPosition.lng = position.coords.longitude;
      this.zoom = 18;
    });
  }

  private getRestaurants() {
    const context = this;
    navigator.geolocation.getCurrentPosition(position => {
      const url =
        "https://shitfoodapi.azurewebsites.net/api/getshit?lat=" +
        position.coords.latitude.toString() +
        "&lng=" +
        position.coords.longitude.toString();
      Axios.get(url).then(function(response) {
        context.places = response.data as object[];
      });
    });
  }

  private getPlacePosition(place: any) {
    return new L.LatLng(place.lat, place.lng);
  }

  private async created() {
    this.getPosition();
    this.getRestaurants();
  }
}
</script>
