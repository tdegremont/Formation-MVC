"use strict";
$(() => {
    // Cette fonction sera exécutée une fois le documebnt chargé
    // Aller chercher l'éléménet avec id="listeEmployes"
    $("#listeEmployes").load("/Employe?partial=true");
    // En cas de changement de valeur dans le formulaire
    $("#formulaireRecherche").on('input', () => {
        var _a;
        $("#listeEmployes").html("En attente...");
        let texte = $("#Texte").val();
        let anciennete = $("#Anciennete").val();
        $("#listeEmployes").load((_a = "/Employe?partial=true&texte=" + (texte !== null && texte !== void 0 ? texte : "") + "&Anciennete=" + anciennete) !== null && _a !== void 0 ? _a : "");
    });
});
