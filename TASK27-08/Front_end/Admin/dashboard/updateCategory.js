var n = localStorage.getItem("categoryID");
var form = document.getElementById("formTest");


const url = `https://localhost:44348/api/Category/getById/${n}`;
const url2 = `https://localhost:44348/api/Category/${n}`;
async function allCategory() {
    debugger;

    var requist = await fetch(url);
    var response = await requist.json();

    

    
        


    form.innerHTML += `

    
    
    <div class="form-group">
        <label for="exampleInputEmail1">Category Name</label>
        <input name="categoryName" type="text" class="form-control" id="exampleInputEmail1" value="${response.categoryName}">
    </div>
    <br />
        
            <div class="form-group">
                    <img class="card-img-top" src="../../../Back_end/TASK21-08/Uploads/${response.categoryImage}" alt="Card image cap" style= "width:200px;">
                <br/> <label for="exampleInputPassword1">Category Image</label>
                <input name="categoryImage" type="file" class="form-control" id="exampleInputPassword1">
            </div>
            <button onclick="submitform()" type="submit" class="btn btn-primary mt-3">Submit</button>
       
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