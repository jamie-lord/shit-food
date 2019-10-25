<template>
  <div class="home">
    <l-map style="width: 100vw; height: 100vh">
      <l-tile-layer :url="'http://{s}.tile.osm.org/{z}/{x}/{y}.png'"></l-tile-layer>
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
  private async created() {

  }
}
</script>
