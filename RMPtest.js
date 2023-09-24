var MyModule = (function () {
    // Soukromá proměnná
    var privateVar = "I am private";

    // Soukromá funkce
    function privateFunction() {
        console.log("This is a private function!");
    }

    // Veřejná funkce
    function publicFunction() {
        console.log("This is a public function!");
    }

    // Vracení objektu, který odhaluje veřejné části modulu
    return {
        publicFunction: publicFunction
    };
})();

// Použití modulu
MyModule.publicFunction(); // Vypíše: "This is a public function!"