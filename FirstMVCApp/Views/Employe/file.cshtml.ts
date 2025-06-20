var a = 1;
var b = 2;
var c = a + b;
console.log("The sum of a and b is: " + c);

$("body").append("<p>The sum of a and b is: " + c + "</p>"); 
$.ready(function () {
    console.log("Document is ready!");
    $("body").append("<p>Document is ready!</p>");
});
