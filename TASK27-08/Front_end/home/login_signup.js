var signUpURL = "https://localhost:44348/api/Users/register"

async function signUp(){
    var signup = document.getElementById("signup");
    var pass = document.getElementById("pass");
    var conf = document.getElementById("conf");
    event.preventDefault();

    if(pass.value!==conf.value){
        alert("Passwords do not match");
        return;
    }else{
    
    const formData = new FormData(signup);
    var requist = await fetch(signUpURL,{
        method: "POST",
        body: formData
    });

    location.reload();
}

}

async function signin(){
    debugger;
    var signin = document.getElementById("signin");
    event.preventDefault();
    
    const formData = new FormData(signin);
    try{
    var requist = await fetch("https://localhost:44348/api/Users/login",{
        method: "POST",
        body: formData
    })
    if(requist.ok){
        var user = await requist.json();

            // Access and store the cartId in localStorage
            localStorage.setItem("cartID", user.cart.cartId);

            // Store the entire user information in localStorage
            localStorage.setItem("userInfo", JSON.stringify(user));
            
            // Redirect to the home page
            location.href = "home.html";
    }else{
        alert("Invalid credentials");
    }
}
    catch{
        alert("Network error");
    }


}