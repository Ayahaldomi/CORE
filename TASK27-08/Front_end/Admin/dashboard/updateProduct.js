var n = localStorage.getItem("productId");
var form = document.getElementById("formTest");


const url = `https://localhost:44348/api/Products/${n}`;
const url2 = `https://localhost:44348/api/Products/${n}`;


async function allCategory() {
    debugger;

    var requist = await fetch(url);
    var response = await requist.json();

    

    
        


    form.innerHTML += `

    
    
    <div class="form-group">
        <label for="exampleInputEmail1">productName</label>
        <input name="productName" type="text" class="form-control" id="exampleInputEmail1" value="${response.productName}">
    </div>
    <div class="form-group">
        <label for="exampleInputEmail1">price</label>
        <input name="price" type="text" class="form-control" id="exampleInputEmail1" value="${response.price}">
    </div>
    <div class="form-group">description</label>
        <input name="description" type="text" class="form-control" id="exampleInputEmail1" value="${response.description}">
    </div>
    <div class="form-group">
                <label for="exampleInputEmail1">CategoryId</label>
                <!-- <input name="CategoryId" type="number" class="form-control" id="exampleInputEmail1" aria-describedby="emailHelp"> -->
                <select name="CategoryId" id="CategoryId" class="form-control custom-select">
    
                </select>
              </div>

        <div class="card-body">
            <div class="form-group">
                    <img class="card-img-top" src="../../../Back_end/TASK21-08/Uploads/${response.productImage}" alt="${response.productImage}" style= "width:150px;">
<br />
                <label for="exampleInputPassword1">Category Image</label>
                <input name="productImage" type="file" class="form-control" id="productImage">
            </div>
            <button onclick="submitform()" type="submit" class="btn btn-primary mt-3">Submit</button>
        </div>
    </div>

    
    


    `
    dropDown();
   
    
}
allCategory();

async function submitform() {

    debugger;
    console.log("hi");
    event.preventDefault();
    
    const formData = new FormData(form);
    var requist = await fetch(url2,{
        method: "PUT",
        body: formData
    });

    var data = requist;
}

const urlDropDown = "https://localhost:44348/api/Category";
async function dropDown() {

    var requist = await fetch(urlDropDown);
    var response = await requist.json();

    var CategoryId = document.getElementById("CategoryId");

    response.forEach(element => {
        


    CategoryId.innerHTML += `
    <option value="${element.categoryId}">${element.categoryName}</option>

    `
    });
    
}
