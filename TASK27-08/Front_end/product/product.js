
const number = Number(localStorage.getItem('categoryID'));

if (number == 0) {
    var url = "https://localhost:44348/api/Products";
}
else{
   var url = `https://localhost:44348/ProductByCategoryId/${number}`;
}


async function allProducts() {
    debugger

    var requist = await fetch(url);
    var response = await requist.json();

    var container = document.getElementById("contriner");

    response.forEach(element => {

        container.innerHTML += `<div class="card" style="width: 18rem;">
  <img src="${element.productImage}" class="card-img-top" alt="...">
  <div class="card-body">
    <h5 class="card-title">${element.productName}</h5>
    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
    <a href="productDEtails.html" class="btn btn-primary" onclick="productID(${element.productId})">Go somewhere</a>
  </div>
</div>`
        
    
    });
    
}

allProducts();

function productID(data){

    localStorage.setItem("productId", data);
}