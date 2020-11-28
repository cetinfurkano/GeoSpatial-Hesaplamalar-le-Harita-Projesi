//// EVENTT

mymap.on('pm:create', function (e) {
    mymap.removeLayer(e.layer);
    theCollection.addLayer(e.layer);
    modalOpen(e);
});


mymap.on('pm:globaleditmodetoggled', function (e) {
    if (!e.enabled) {
        updateModalOpen();
    }
});


$("#btnKapat").click(function () {
    var layer = JSON.parse($(this).attr("data-layer"));
    theCollection.removeLayer(layer.properties._leaflet_id);

});


$("#btnKaydet").click(function () {
    var forms = document.getElementsByClassName('needs-validation');
    if (forms[0].checkValidity()) {
        var layer = JSON.parse($(this).attr("data-layer"));
        var info = $("#txtGiris").val();
        addDatabase(info, layer);
    }

});

$("#btnUpdateKaydetme").click(function () {
    var layers = JSON.parse($(this).attr("data-layer"));
    takeOver(layers);

});


$("#btnUpdateKaydet").click(function () {

    var layers = JSON.parse($(this).attr("data-layer"));
    var points = [];
    var polygons = [];

    for (var i = 0; i < layers.length; i++) {
        
        var geoJson = theCollection.getLayer(layers[i].leaflet).toGeoJSON();
        if (geoJson.geometry.type == "Point") {
            points.push({ id: layers[i].id, geometry: geoJson.geometry });
        }
        else if (geoJson.geometry.type == "Polygon") {
            polygons.push({ id: layers[i].id, geometry: geoJson.geometry });
        }
    }
    var model = { points, polygons };
    updateData(model,layers);
  });

$("#value").focus(function () {

    if (change && Object.keys(theCollection._layers).length > 0) {
        ajaxGET({
            url: "/Home/BringAddress"
        }).done(function (d) {
            addresses = d;
            change = false;
            autocomplete(document.getElementById("value"), addresses);
        });
    }

});

document.addEventListener('click', function (e) {
    if (e.target && e.target.matches(".autocomplete-items div")) {
        $("#value").focus();
    }
});

$("#search").click(function () {
    if ($(this).attr("data-fly") != "") {
        var coords = JSON.parse($(this).attr("data-fly"));
        mymap.flyTo(coords, 18);
        $(this).attr("data-fly", "");
    }
    else {
        Alert_Error("Böyle bir adres bulunamadı");
    }
});

$(".check_group input[type='checkbox']").click(function () {
    if ($(this).is(':checked')) {
        loadData(this.id);
    }
    else {
        deleteData(this.id);
    }

});

document.addEventListener('mousedown', function (e) {
    console.log(e.target);
    if (e.target && !((e.target.matches("#value") || (e.target.matches(".autocomplete-items"))
        || (e.target.matches(".autocomplete-items div"))))) {

        $('.autocomplete-items div').hide();
    }

});

///// EVENT BİTİŞ


