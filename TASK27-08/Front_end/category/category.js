const url = "https://localhost:44348/api/Category";

async function allCategory() {

    var requist = await fetch(url);
    var response = await requist.json();

    var container = document.getElementById("contriner");

    response.forEach(element => {
        


    container.innerHTML += `
        <div class="card" style="width: 18rem;">
  <img class="card-img-top" src="${element.categoryImage}" alt="Card image cap">
  <div class="card-body">
    <h5 class="card-title">${element.categoryName}</h5>
    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
    <a href="../product/product.html" class="btn btn-primary" onclick="storeData(${element.categoryId})">Go somewhere</a>
        <a href="updateCategory.html" class="btn btn-primary" onclick="storeData(${element.categoryId})">Go somewhere</a>

  </div>
</div>
    `
    });
    
}

function storeData(data){
  
    localStorage.setItem("categoryID", data);
    console.log("hi")
}

allCategory();