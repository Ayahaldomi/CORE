var n = localStorage.getItem("categoryID");
var form = document.getElementById("formTest");


const url = `https://localhost:44348/api/Category/getById/${n}`;
const url2 = `https://localhost:44348/api/Category/${n}`;
async function allCategory() {
    debugger;

    var requist = await fetch(url);
    var response = await requist.json();

    

    
        


    form.innerHTML += `

    
    <div class="card" style="width: 18rem; margin-top: 20px;">
    <div class="form-group">
        <label for="exampleInputEmail1">Category Name</label>
        <input name="categoryName" type="text" class="form-control" id="exampleInputEmail1" value="${response.categoryName}">
        <small id="emailHelp" class="form-text text-muted">We'll never share your email with anyone else.</small>
    </div>
        <img class="card-img-top" src="../../Back_end/TASK21-08/Uploads/${response.categoryImage}" alt="Card image cap">
        <div class="card-body">
            <div class="form-group">
                <label for="exampleInputPassword1">Category Image</label>
                <input name="categoryImage" type="file" class="form-control" id="exampleInputPassword1">
            </div>
            <button onclick="submitform()" type="submit" class="btn btn-primary mt-3">Submit</button>
        </div>
    </div>
    


    `
   
    
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