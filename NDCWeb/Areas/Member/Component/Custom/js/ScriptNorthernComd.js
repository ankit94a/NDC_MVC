function CategorySelectAction() {
	var category = document.getElementById("category").value;
	switch (category) {
		case 'all-categories':
			document.getElementById("sub-category").innerHTML = "<option value='all-sub-categories'>All Sub Categories</option><option value='children'>Children Products</option><option value='women'>Women Products</option><option value='home-decor'>Home Decoration Products</option><option value='kitchen-dining'>Kitchen & Dining Products</option>			<option value='stationary'>Stationary Products</option><option value='convenience'>Convenience Products</option>";
			var colcard = document.getElementsByClassName("custom-card");
			for (i = 0; i < colcard.length; i++) {
				colcard[i].style.display = 'block';
			}
			break;
		case 'clothing-fashion':
			document.getElementById("sub-category").innerHTML = "<option value=''>Select Sub-Category</option><option value='children'>Children Products</option><option value='women'>Women Products</option>";
			var colcard = document.getElementsByClassName("custom-card");
			for (i = 0; i < colcard.length; i++) {
				colcard[i].style.display = 'none';
			}
			var clothingfashioncard = document.getElementsByClassName("clothing-fashion");
			for (j = 0; j < clothingfashioncard.length; j++) {
				clothingfashioncard[j].style.display = 'block';
			}
			break;
		case 'home-furnishing':
			document.getElementById("sub-category").innerHTML = "<option value=''>Select Sub-Category</option><option value='home-decor'>Home Decoration Products</option><option value='kitchen-dining'>Kitchen & Dining Products</option>";
			var colcard = document.getElementsByClassName("custom-card");
			for (i = 0; i < colcard.length; i++) {
				colcard[i].style.display = 'none';
			}
			var homefurnishingcard = document.getElementsByClassName("home-furnishing");
			for (j = 0; j < homefurnishingcard.length; j++) {
				homefurnishingcard[j].style.display = 'block';
			}
			break;
		case 'misc':
			document.getElementById("sub-category").innerHTML = "<option value=''>Select Sub-Category</option><option value='stationary'>Stationary Products</option><option value='convenience'>Convenience Products</option>";
			var colcard = document.getElementsByClassName("custom-card");
			for (i = 0; i < colcard.length; i++) {
				colcard[i].style.display = 'none';
			}
			var misccard = document.getElementsByClassName("misc");
			for (j = 0; j < misccard.length; j++) {
				misccard[j].style.display = 'block';
			}
			break;
	}
}
function SubCategorySelectAction() {
	var subcategory = document.getElementById("sub-category").value;
	switch (subcategory) {
		case 'all-sub-categories':
			document.getElementById("category").innerHTML = "<option value='all-categories' selected>All Categories</option><option value='clothing-fashion'>Clothing & Fashion Products</option><option value='home-furnishing'>Home Furnishing Products</option><option value='misc'>Misc Products</option>";
			var colcard = document.getElementsByClassName("custom-card");
			for (i = 0; i < colcard.length; i++) {
				colcard[i].style.display = 'block';
			}
			break;
		case 'children':
			document.getElementById("category").innerHTML = "<option value='all-categories'>All Categories</option><option value='clothing-fashion' selected>Clothing & Fashion Products</option><option value='home-furnishing'>Home Furnishing Products</option><option value='misc'>Misc Products</option>";
			var colcard = document.getElementsByClassName("custom-card");
			for (i = 0; i < colcard.length; i++) {
				colcard[i].style.display = 'none';
			}
			var clothingfashioncard = document.getElementsByClassName("children");
			for (j = 0; j < clothingfashioncard.length; j++) {
				clothingfashioncard[j].style.display = 'block';
			}
			break;
		case 'women':
			document.getElementById("category").innerHTML = "<option value='all-categories'>All Categories</option><option value='clothing-fashion' selected>Clothing & Fashion Products</option><option value='home-furnishing'>Home Furnishing Products</option><option value='misc'>Misc Products</option>";
			var colcard = document.getElementsByClassName("custom-card");
			for (i = 0; i < colcard.length; i++) {
				colcard[i].style.display = 'none';
			}
			var homefurnishingcard = document.getElementsByClassName("women");
			for (j = 0; j < homefurnishingcard.length; j++) {
				homefurnishingcard[j].style.display = 'block';
			}
			break;
		case 'home-decor':
			document.getElementById("category").innerHTML = "<option value='all-categories'>All Categories</option><option value='clothing-fashion'>Clothing & Fashion Products</option><option value='home-furnishing' selected>Home Furnishing Products</option><option value='misc'>Misc Products</option>";
			var colcard = document.getElementsByClassName("custom-card");
			for (i = 0; i < colcard.length; i++) {
				colcard[i].style.display = 'none';
			}
			var misccard = document.getElementsByClassName("home-decor");
			for (j = 0; j < misccard.length; j++) {
				misccard[j].style.display = 'block';
			}
			break;
		case 'kitchen-dining':
			document.getElementById("category").innerHTML = "<option value='all-categories'>All Categories</option><option value='clothing-fashion'>Clothing & Fashion Products</option><option value='home-furnishing' selected>Home Furnishing Products</option><option value='misc'>Misc Products</option>";
			var colcard = document.getElementsByClassName("custom-card");
			for (i = 0; i < colcard.length; i++) {
				colcard[i].style.display = 'none';
			}
			var misccard = document.getElementsByClassName("kitchen-dining");
			for (j = 0; j < misccard.length; j++) {
				misccard[j].style.display = 'block';
			}
			break;
		case 'stationary':
			document.getElementById("category").innerHTML = "<option value='all-categories'>All Categories</option><option value='clothing-fashion'>Clothing & Fashion Products</option><option value='home-furnishing'>Home Furnishing Products</option><option value='misc' selected>Misc Products</option>";
			var colcard = document.getElementsByClassName("custom-card");
			for (i = 0; i < colcard.length; i++) {
				colcard[i].style.display = 'none';
			}
			var misccard = document.getElementsByClassName("stationary");
			for (j = 0; j < misccard.length; j++) {
				misccard[j].style.display = 'block';
			}
			break;
		case 'convenience':
			document.getElementById("category").innerHTML = "<option value='all-categories'>All Categories</option><option value='clothing-fashion'>Clothing & Fashion Products</option><option value='home-furnishing'>Home Furnishing Products</option><option value='misc' selected>Misc Products</option>";
			var colcard = document.getElementsByClassName("custom-card");
			for (i = 0; i < colcard.length; i++) {
				colcard[i].style.display = 'none';
			}
			var misccard = document.getElementsByClassName("convenience");
			for (j = 0; j < misccard.length; j++) {
				misccard[j].style.display = 'block';
			}
			break;
	}
}