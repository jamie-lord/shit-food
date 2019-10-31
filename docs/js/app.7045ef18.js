(function(e){function t(t){for(var o,i,c=t[0],s=t[1],u=t[2],l=0,f=[];l<c.length;l++)i=c[l],Object.prototype.hasOwnProperty.call(r,i)&&r[i]&&f.push(r[i][0]),r[i]=0;for(o in s)Object.prototype.hasOwnProperty.call(s,o)&&(e[o]=s[o]);p&&p(t);while(f.length)f.shift()();return a.push.apply(a,u||[]),n()}function n(){for(var e,t=0;t<a.length;t++){for(var n=a[t],o=!0,i=1;i<n.length;i++){var s=n[i];0!==r[s]&&(o=!1)}o&&(a.splice(t--,1),e=c(c.s=n[0]))}return e}var o={},r={app:0},a=[];function i(e){return c.p+"js/"+({about:"about"}[e]||e)+"."+{about:"3de71244"}[e]+".js"}function c(t){if(o[t])return o[t].exports;var n=o[t]={i:t,l:!1,exports:{}};return e[t].call(n.exports,n,n.exports,c),n.l=!0,n.exports}c.e=function(e){var t=[],n=r[e];if(0!==n)if(n)t.push(n[2]);else{var o=new Promise((function(t,o){n=r[e]=[t,o]}));t.push(n[2]=o);var a,s=document.createElement("script");s.charset="utf-8",s.timeout=120,c.nc&&s.setAttribute("nonce",c.nc),s.src=i(e);var u=new Error;a=function(t){s.onerror=s.onload=null,clearTimeout(l);var n=r[e];if(0!==n){if(n){var o=t&&("load"===t.type?"missing":t.type),a=t&&t.target&&t.target.src;u.message="Loading chunk "+e+" failed.\n("+o+": "+a+")",u.name="ChunkLoadError",u.type=o,u.request=a,n[1](u)}r[e]=void 0}};var l=setTimeout((function(){a({type:"timeout",target:s})}),12e4);s.onerror=s.onload=a,document.head.appendChild(s)}return Promise.all(t)},c.m=e,c.c=o,c.d=function(e,t,n){c.o(e,t)||Object.defineProperty(e,t,{enumerable:!0,get:n})},c.r=function(e){"undefined"!==typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(e,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(e,"__esModule",{value:!0})},c.t=function(e,t){if(1&t&&(e=c(e)),8&t)return e;if(4&t&&"object"===typeof e&&e&&e.__esModule)return e;var n=Object.create(null);if(c.r(n),Object.defineProperty(n,"default",{enumerable:!0,value:e}),2&t&&"string"!=typeof e)for(var o in e)c.d(n,o,function(t){return e[t]}.bind(null,o));return n},c.n=function(e){var t=e&&e.__esModule?function(){return e["default"]}:function(){return e};return c.d(t,"a",t),t},c.o=function(e,t){return Object.prototype.hasOwnProperty.call(e,t)},c.p="/",c.oe=function(e){throw console.error(e),e};var s=window["webpackJsonp"]=window["webpackJsonp"]||[],u=s.push.bind(s);s.push=t,s=s.slice();for(var l=0;l<s.length;l++)t(s[l]);var p=u;a.push([0,"chunk-vendors"]),n()})({0:function(e,t,n){e.exports=n("cd49")},cd49:function(e,t,n){"use strict";n.r(t);n("e260"),n("e6cf"),n("cca6"),n("a79d");var o=n("2b0e"),r=function(){var e=this,t=e.$createElement,n=e._self._c||t;return n("div",{attrs:{id:"app"}},[n("router-view")],1)},a=[],i=n("9f12"),c=n("8b83"),s=n("c65a"),u=n("c03e"),l=n("9ab4"),p=n("60a3"),f=function(e){function t(){return Object(i["a"])(this,t),Object(c["a"])(this,Object(s["a"])(t).apply(this,arguments))}return Object(u["a"])(t,e),t}(p["b"]);f=l["a"]([Object(p["a"])({components:{}})],f);var g=f,d=g,h=n("2877"),m=Object(h["a"])(d,r,a,!1,null,null,null),v=m.exports,b=(n("d3b7"),n("8c4f")),y=function(){var e=this,t=e.$createElement,n=e._self._c||t;return n("div",{staticClass:"home"},[n("l-map",{staticStyle:{width:"100vw",height:"100vh"},attrs:{center:e.myPosition,zoom:e.zoom}},[n("l-tile-layer",{attrs:{url:"https://{s}.tile.openstreetmap.se/hydda/full/{z}/{x}/{y}.png"}}),n("l-marker",{attrs:{latLng:e.myPosition,icon:e.myIcon,alt:"Current marker",title:"This is you"}}),e._l(e.places,(function(t){return n("l-marker",{key:t.id,attrs:{latLng:e.getPlacePosition(t),icon:e.cutleryIcon,options:e.getPlaceMarkerOptions(t)}},[n("l-popup",{attrs:{content:e.getPlacePopupContent(t)}})],1)}))],2)],1)},w=[],P=(n("0d03"),n("b0c0"),n("25f0"),n("96cf"),n("89ba")),k=n("53fe"),j=n("e11e"),O=n.n(j),L=n("2699"),S=n("a40a"),_=n("4e2b"),x=n("f60f"),I=n("bc3a"),R=n.n(I),C=function(e){function t(){var e;return Object(i["a"])(this,t),e=Object(c["a"])(this,Object(s["a"])(t).apply(this,arguments)),e.myPosition=new O.a.LatLng(0,0),e.zoom=4,e.places=[],e.myIcon=new O.a.Icon({iconUrl:"img/markers/me.svg",iconSize:[36,36],iconAnchor:[18,18],popupAnchor:[34,0]}),e.cutleryIcon=new O.a.Icon({iconUrl:"img/markers/cutlery.svg",iconSize:[36,36],iconAnchor:[18,18],popupAnchor:[34,0]}),e}return Object(u["a"])(t,e),Object(k["a"])(t,[{key:"getPosition",value:function(){var e=this;navigator.geolocation.getCurrentPosition((function(t){e.myPosition.lat=t.coords.latitude,e.myPosition.lng=t.coords.longitude,e.zoom=18}))}},{key:"getRestaurants",value:function(){var e=this;navigator.geolocation.getCurrentPosition((function(t){var n="https://shitfoodapi.azurewebsites.net/api/getshit?lat="+t.coords.latitude.toString()+"&lng="+t.coords.longitude.toString();R.a.get(n).then((function(t){e.places=t.data}))}))}},{key:"getPlacePosition",value:function(e){return new O.a.LatLng(e.lat,e.lng)}},{key:"getPlaceMarkerOptions",value:function(e){return{title:e.name,alt:e.name}}},{key:"getPlacePopupContent",value:function(e){return"<strong>"+e.name+'</strong><br><a href="https://ratings.food.gov.uk/business/en-GB/'+e.foodHygieneRatingId+'" target="_blank" rel="noreferrer nofollow">Food Hygiene Rating: <strong>'+e.foodHygieneRating+"</strong> ("+this.hygieneRatingPhrase(e.foodHygieneRating)+")</a>"}},{key:"hygieneRatingPhrase",value:function(e){switch(e){case"2":return"Improvement Necessary";case"1":return"Major Improvement Necessary";case"0":return"Urgent Improvement Necessary"}}},{key:"created",value:function(){var e=Object(P["a"])(regeneratorRuntime.mark((function e(){return regeneratorRuntime.wrap((function(e){while(1)switch(e.prev=e.next){case 0:this.getPosition(),this.getRestaurants();case 2:case"end":return e.stop()}}),e,this)})));function t(){return e.apply(this,arguments)}return t}()}]),t}(p["b"]);C=l["a"]([Object(p["a"])({components:{LMap:L["a"],LTileLayer:S["a"],LMarker:_["a"],LPopup:x["a"]}})],C);var M=C,z=M,A=Object(h["a"])(z,y,w,!1,null,null,null),T=A.exports;o["a"].use(b["a"]);var E=[{path:"/",name:"home",component:T},{path:"/about",name:"about",component:function(){return n.e("about").then(n.bind(null,"f820"))}}],N=new b["a"]({mode:"history",base:"/",routes:E}),H=N,F=n("2f62");o["a"].use(F["a"]);var U=new F["a"].Store({state:{},mutations:{},actions:{},modules:{}}),$=n("9483");Object($["a"])("".concat("/","service-worker.js"),{ready:function(){console.log("App is being served from cache by a service worker.\nFor more details, visit https://goo.gl/AFskqB")},registered:function(){console.log("Service worker has been registered.")},cached:function(){console.log("Content has been cached for offline use.")},updatefound:function(){console.log("New content is downloading.")},updated:function(){console.log("New content is available; please refresh.")},offline:function(){console.log("No internet connection found. App is running in offline mode.")},error:function(e){console.error("Error during service worker registration:",e)}}),o["a"].config.productionTip=!1,new o["a"]({router:H,store:U,render:function(e){return e(v)}}).$mount("#app")}});
//# sourceMappingURL=app.7045ef18.js.map