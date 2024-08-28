function clearLocal(){
    localStorage.clear();
    alert("All data has been cleared from local storage.");
}

function cartID(){
    localStorage.setItem("cartID", 1)
    localStorage.setItem("userID", 1)
}