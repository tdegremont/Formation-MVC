$(() => {
    // Cette fonction sera exécutée une fois le documebnt chargé

    // Aller chercher l'éléménet avec id="listeEmployes"
    $("#listeEmployes").load("/Employe?partial=true");
    
    // En cas de changement de valeur dans le formulaire
    $("#formulaireRecherche").on('input', () => {
        $("#listeEmployes").html("En attente...");
        let texte = $("#Texte").val() ?? "";
        let anciennete = $("#Anciennete").val() ?? "";
        $("#listeEmployes").load("/Employe?partial=true&texte=" + texte + "&Anciennete=" + anciennete);
    })
})