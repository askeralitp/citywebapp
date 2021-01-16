$(document).ready(function () {

	var host = "https://" + window.location.host;

	var headers = {};
	var citiesEndpoint = "/api/cities";
	var countriesEndpoint = "/api/countries";


	// pripremanje dogadjaja za brisanje
	$("body").on("click", "#btnDelete", deleteCity);

	



	loadCities();


	//funkcija ucitavanja gradova
	function loadCities() {
		var requestUrl = host + citiesEndpoint;
		$.getJSON(requestUrl, setCities);
	}


	// metoda za postavljanje gradova u tabelu
	function setCities(data, status) {

		var $container = $("#data");
		$container.empty();

		if (status === "success") {
			// ispis naslova
			var div = $("<div></div>");
			var h1 = $("<h1 style='text-align:center'>Cities</h1>");
			div.append(h1);
			// ispis tabele
			var table = $("<table class='table table-bordered' ></table>");


			var header = $("<thead><tr style='background-color:orange'><td>Id</td><td>Name</td><td>Zip code</td><td>Population</td><td>Country</td><td>Delete</td></tr></thead>");


			table.append(header);
			tbody = $("<tbody></tbody>");
			for (i = 0; i < data.length; i++) {
				// prikazujemo novi red u tabeli
				var row = "<tr>";
				// prikaz podataka
				var displayData = "<td>" + data[i].Id + "</td><td>" + data[i].Name + "</td><td>" + data[i].Zip + "</td><td>" + data[i].Population + "</td><td>" + data[i].Country.Name  + "</td>";
				// prikaz dugmeta za brisanje
				var stringId = data[i].Id.toString();
				var displayDelete = "<td><button class='btn btn-danger' id=btnDelete name=" + stringId + ">Delete</button></td>";
				
				

				row += displayData + displayDelete + "</tr>";

				// dodati red
				tbody.append(row);
			}
			table.append(tbody);

			div.append(table);

			$container.append(div);
		}
	}



	//popunjavanje selekta za dodavanje
	$.ajax({
		"type": "GET",
		"url": host + countriesEndpoint
	}).done(function (data, status) {
		var select = $("#country");
		for (var i = 0; i < data.length; i++) {
			var option = "<option value='" + data[i].Id + "'>" + data[i].Name +  "</option>";
			select.append(option);
		}
	});

	


	// dodavanje novog grada
	$("#addCityForm").submit(function (e) {
		// sprecavanje default akcije forme
		e.preventDefault();


		var name = $("#nameCity").val();
		var zip = $("#zipCode").val();
		var population = $("#population").val();
		var country = $("#country").val();

		var sendData = {
			"Name": name,
			"Zip": zip,
			"Population": population,
			"CountryId": country
		};

		$.ajax({
			"type": "POST",
			"url": host + citiesEndpoint,
			"headers": headers,
			"data": sendData
		})
			.done(function (data, status) {
				$("#nameCity").val("");
				$("#zipCode").val("");
				$("#population").val("");
				$("#country").val("1");

				loadCities();
			}).fail(function (data, status) {
				alert("Error!");
			});
	});



	// dodavanje nove drzave
	$("#addCountryForm").submit(function (e) {
		// sprecavanje default akcije forme
		e.preventDefault();


		var name = $("#nameCountry").val();
		var code = $("#countryCode").val().toUpperCase();
		


		var sendObject = {
			"Name": name,
			"Code": code

		};

		$.ajax({
			type: "POST",
			url: host + countriesEndpoint,
			headers: headers,
			data: sendObject
		})
			.done(function (data, status) {
				$("#nameCountry").val("");
				$("#countryCode").val("");
		

				loadCountries();
			}).fail(function (data, status) {
				alert("Error!");
			});
	});



	//brisanje grada
	function deleteCity() {

		var deleteID = this.name;



		// saljemo zahtev 
		$.ajax({
			url: host + citiesEndpoint + "/" + deleteID.toString(),
			type: "DELETE",
			headers: headers
		})
			.done(function (data, status) {
				loadCities();
			})
			.fail(function (data, status) {
				alert("Error!");
			});
	}


	//pretraga
	$("#pretraga").submit(function (e) {
		e.preventDefault();
		

		var from = $("#from").val();
		var to = $("#to").val();

		var sendData = {
			"Min": from,
			"Max": to
		};

		$.ajax({
			"type": "POST",
			"url": host + "/api/search",
			"data": sendData,
			"headers": headers
		}).done(function (data, status) {
			$("#from").val("");
			$("#to").val("");

			setCities(data, status);
		}).fail(function (data, status) { alert("Error"); });
	});

	

	$("#refreshTable").click(function () {
		location.reload();
		
	});








});
