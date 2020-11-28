////    MODALL

var form1 = {};

function modalOpen(e) {

    var title = $("#girisBaslik");
    var label = $("label[for='txtGiris']");
    var txt = $("#txtGiris");

    if (e.shape == "Marker") {
        title.html("Kapı Bilgileri");
        label.html("Kapı No");
        txt.attr("placeholder", "Kapi no giriniz.");
    }
    else {
        title.html("Mahalle Bilgileri");
        label.html("Mahalle No");
        txt.attr("placeholder", "Mahalle no giriniz.");
    }


    $("#myModal").modal({ backdrop: "static" });

    var layer = e.layer.toGeoJSON();
    layer.properties._leaflet_id = e.layer._leaflet_id;
    
    
    $("#btnKaydet").attr("data-layer", JSON.stringify(layer));
    $("#btnKapat").attr("data-layer", JSON.stringify(layer));

    bagla();
}

function bagla() {
    
    var forms = document.getElementsByClassName('needs-validation');
    
    var validation = Array.prototype.filter.call(forms, function (form) {
        form1 = form;
        document.getElementById("btnKaydet").addEventListener('click', function (event) {
            if (form.checkValidity() === false) {
                event.preventDefault();
                event.stopPropagation();
            }
            form.classList.add('was-validated');

        });

    });
}

function updateModalOpen() {
  
    $("#updateModal").modal({ backdrop: "static" }); 

    var stringLayers = JSON.stringify(layersToUpdate);

    $("#btnUpdateKaydet").attr("data-layer", stringLayers);
    $("#btnUpdateKaydetme").attr("data-layer", stringLayers);

    layersToUpdate = [];
}

 //// MODAL BİTİŞ