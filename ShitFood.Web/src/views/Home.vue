<template>
  <div class="home">
    <l-map style="width: 100vw; height: 100vh" :center="myPosition" :zoom="4">
      <l-tile-layer :url="'http://{s}.tile.osm.org/{z}/{x}/{y}.png'"></l-tile-layer>
      <l-marker :latLng="myPosition"></l-marker>
    </l-map>
  </div>
</template>

<script lang="ts">
import { Component, Emit, Mixins, Model, Prop, Vue } from "vue-property-decorator";
import L from 'leaflet';
import { LMap, LTileLayer, LMarker } from 'vue2-leaflet';

@Component({
  components: { LMap, LTileLayer, LMarker }
})
export default class Home extends Vue {
  private myPosition: L.LatLng = new L.LatLng(52, 0);

  private getPosition() {
    navigator.geolocation.getCurrentPosition(position => {
      this.myPosition.lat = position.coords.latitude;
      this.myPosition.lng = position.coords.longitude;
      console.log(position);
    })
  }

  private async created() {
    // new LMarker()
    this.getPosition();
  }
}
</script>
