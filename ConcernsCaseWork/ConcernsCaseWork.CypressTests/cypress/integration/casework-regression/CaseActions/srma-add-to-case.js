import AddToCasePage from "/cypress/pages/caseActions/addToCasePage";
import CaseManagementPage from "/cypress/pages/caseMangementPage";
import utils from "/cypress/support/utils"
import FinancialPlanPage from "/cypress/pages/caseActions/financialPlanPage";
import srmaAddPage from "/cypress/pages/caseActions/srmaAddPage";
import srmaEditPage from "/cypress/pages/caseActions/srmaEditPage";

describe("User can add case actions to an existing case", () => {
	before(() => {
		cy.login();
	});

	afterEach(() => {
		cy.storeSessionData();
	});

	const searchTerm ="Accrington St Christopher's Church Of England High School";
	let term = "";
	let $status = "";
	let returnedDate2 = ["date1", "date2"];
	//let returnedDate = "null";
	let returnedDate = ["date1", "date2"];
	let returnedEndDate = "";
	let arrDate = ["day1", "month1", "year1","day2", "month2", "year2", ];
	let stText = "null";
	let concatDate = "";

	it("User enters the case page", () => {
		cy.checkForExistingCase();
	});

	it("User has option to select an SRMA Case Action", () => {
		cy.reload();
		CaseManagementPage.getAddToCaseBtn().click();
		AddToCasePage.addToCase('Srma')
		AddToCasePage.getCaseActionRadio('Srma').siblings().should('contain.text', AddToCasePage.actionOptions[8]);

	});



	it("User can click to add Financial Plan Case Action to a case", function () {
		
		AddToCasePage.getAddToCaseBtn().click();
		
		//cy.wait(2000);


		cy.log(utils.checkForGovErrorSummaryList() );

		if (utils.checkForGovErrorSummaryList() > 0 ) { 
			cy.log("Case Action already exists");

					cy.visit(Cypress.env('url'), { timeout: 30000 });
					cy.checkForExistingCase(true);
					CaseManagementPage.getAddToCaseBtn().click();
					AddToCasePage.addToCase('Srma');
					AddToCasePage.getCaseActionRadio('Srma').siblings().should('contain.text', AddToCasePage.actionOptions[8]);
					AddToCasePage.getAddToCaseBtn().click();

		}else {
			cy.log("No Case Action exists");	
			cy.log(utils.checkForGovErrorSummaryList() );

		}

			});


	it("User is taken to the correct Case Action page", function () {

		srmaAddPage.getHeadingText().then(term => {
			expect(term.text().trim()).to.match(/School Resource Management Adviser/i);
		});
	});

	it("User is shown validation and cannot proceed without selecting a status", () => {
		srmaAddPage.setDateOffered();
		srmaAddPage.getAddCaseActionBtn().click();
		utils.getGovErrorSummaryList().should('contain.text', 'Select status');
		cy.reload(true);

	});


	it("User is shown validation and cannot proceed without selecting a valid date", () => {
		//srmaAddPage.setStatusSelect("random").then((returnedVal) => {
		//	cy.wrap(returnedVal.trim()).as("stText");
		//});
	
		srmaAddPage.setStatusSelect("random");
		srmaAddPage.getDateOfferedDay().type("0");
		srmaAddPage.getDateOfferedMonth().type("0");
		srmaAddPage.getDateOfferedYear().type("0");

		srmaAddPage.getAddCaseActionBtn().click();

		utils.getGovErrorSummaryList().should('contain.text', 'Enter the date SRMA was offered to the trust');
		cy.reload(true);

	});


	/*
	it("Date return test", () => {

		srmaAddPage.getDateOfferedDay().type("0").then(($returnedEl) => {
			cy.log($returnedEl)
		});

		cy.get('[id="dtr-month"]').type("0");
		cy.get('[id="dtr-year"]').type("0");
		cy.get('[id="add-srma-button"]').click();
		cy.get('[class="govuk-list govuk-error-summary__list"]').should('contain.text', 'Enter the date SRMA was offered to the trust');
		//cy.reload();
	});
*/



	it("User is shown validation and cannot add more tan 500 characters in the Notes section", () => {
		const lstring =
        'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwx';

		srmaAddPage.getNotesBox().should('be.visible');
		srmaAddPage.getNotesInfo().then(($inf) =>{
            expect($inf).to.be.visible
            expect($inf.text()).to.match(/(500 characters remaining)/i)   

            let text = Cypress._.repeat(lstring, 10)
            expect(text).to.have.length(500);
			
			srmaAddPage.getNotesBox().invoke('val', text);   
			srmaAddPage.getNotesBox().type('{shift}{alt}'+ '1');   
   
		})
	});


	it("User is given a warning for remaining text", () => {
		srmaAddPage.setStatusSelect("random");
		srmaAddPage.setDateOffered();
		srmaAddPage.getNotesInfo().then(($inf) =>{

			expect($inf).to.be.visible
			expect($inf.text()).to.match(/(1 character too many)/i);      
			});

			srmaAddPage.getAddCaseActionBtn().click();
			utils.getGovErrorSummaryList().should('contain.text', 'Notes must be 500 characters or less');
			//cy.reload();
	});


	it("User can select an SRMA status", function () {

		cy.log("setStatusSelect ").then(() => {
			cy.log(srmaAddPage.setStatusSelect("random") ).then((returnedVal) => { 
				cy.wrap(returnedVal.trim()).as("stText").then(() =>{
					stText  = returnedVal;
					cy.log(self.stText);

					cy.log(self.stText);
					cy.log("logging the result "+returnedVal)
					cy.log("logging the result "+self.stText)
					cy.log("logging the result "+stText)
	
				});
				cy.log(self.stText);
				stText  = returnedVal;
				cy.log("logging the result out 0 "+returnedVal)
				cy.log("logging the result out 0"+self.stText)
				cy.log("logging the result out 0 "+stText)


				});
				cy.log("logging the result out 1 "+self.stText)
				cy.log("logging the result out 1 "+self.stText)
				cy.log("logging the result out 1 "+stText)

			});
			cy.log("logging the result out 2 "+self.stText)
			cy.log("logging the result out 2 "+stText)
	
	});



	it("User can enter a valid offered date", function () {

		srmaAddPage.setDateOffered();

		/*
		cy.log("logging date offered closure ").then(() => {
			cy.log(srmaAddPage.setDateOffered() ).then((returnedVal) => { 
				cy.log("logging date offered closure inside nested "+returnedVal).then(() =>{
					cy.wrap(returnedVal.trim()).as("returnedDate").then(() =>{

					//returnedDate = returnedVal;
					cy.log("logging date offered out 0 returnedVal "+returnedVal)
					cy.log("logging date offered out 0 returnedDate "+returnedDate);
				});

			});

		});
	});

		cy.log("logging date offered out 0 +self.stText "+self.stText)
		cy.log("logging date offered out 0returnedDate "+returnedDate);
		*/
	});

/*
	it("User can successfully add valid SRMA actions to a case", () => {

		cy.reload(true);

//SET STATUS
//----------------------
		cy.log("setStatusSelect ").then(() => {
			cy.log(srmaAddPage.setStatusSelect("random") ).then((returnedVal) => { 
				cy.wrap(returnedVal.trim()).as("stText").then(() =>{
					stText  = returnedVal;
					cy.log(self.stText);

					cy.log(self.stText);
					cy.log("logging the result "+returnedVal)
					cy.log("logging the result "+self.stText)
					cy.log("logging the result "+stText)
				});

				cy.log(self.stText);
				stText  = returnedVal;
				cy.log("logging the result out 0 "+returnedVal);
				cy.log("logging the result out 0"+self.stText);
				cy.log("logging the result out 0 "+stText);

				});
				cy.log("logging the result out 1 "+self.stText);
				cy.log("logging the result out 1 "+self.stText);
				cy.log("logging the result out 1 "+stText);

			});
			cy.log("logging the result out 2 "+self.stText);
			cy.log("logging the result out 2 "+stText);
			//cy.log("logging date offered out 0 +self.stText "+this.stText);




//SET DATE
//----------------------

			cy.log("date offered closure ").then(() => {
				cy.log(srmaAddPage.setDateOffered() ).then((returnedVal) => { 
					cy.log("logging date offered closure inside nested "+returnedVal).then(() =>{
						cy.wrap(returnedVal.trim()).as("returnedDate").then(() =>{

						returnedDate = returnedVal;
						cy.log("set Date logging date offered out 0 returnedVal "+returnedVal)
						cy.log("set date logging date offered out 0 returnedDate "+returnedDate);

					});
				});
				returnedDate = returnedVal;

			});

		});
			cy.log("logging date offered out 0 +self.stText "+self.stText);
			//cy.log("logging date offered out 0 +self.stText "+this.stText);
			cy.log("logging date offered out 0returnedDate "+returnedDate);
	
	srmaAddPage.getAddCaseActionBtn().click();

	});

	*/





	it("User can successfully add valid SRMA actions to a case", () => {

		cy.reload(true);

	//SET STATUS
	//----------------------
		cy.log("setStatusSelect ").then(() => {
			cy.log(srmaAddPage.setStatusSelect("random") ).then((returnedVal) => { 
				cy.wrap(returnedVal.trim()).as("stText").then(() =>{
					stText  = returnedVal;
					cy.log(self.stText);

					cy.log(self.stText);
					cy.log("logging the result "+returnedVal)
					cy.log("logging the result "+self.stText)
					cy.log("logging the result "+stText)
				});

				cy.log(self.stText);
				stText  = returnedVal;
				cy.log("logging the result out 0 "+returnedVal);
				cy.log("logging the result out 0"+self.stText);
				cy.log("logging the result out 0 "+stText);

				});
				cy.log("logging the result out 1 "+self.stText);
				cy.log("logging the result out 1 "+self.stText);
				cy.log("logging the result out 1 "+stText);

			});
			cy.log("logging the result out 2 "+self.stText);
			cy.log("logging the result out 2 "+stText);
			//cy.log("logging date offered out 0 +self.stText "+this.stText);




//SET DATE
//----------------------

			cy.log("date offered closure ").then(() => {
				cy.log(srmaAddPage.setDateOffered() ).then((returnedVal) => { 
					cy.log("logging date offered closure inside nested "+returnedVal).then(() =>{
						cy.wrap(returnedVal.trim()).as("returnedDate").then(() =>{

						returnedDate = returnedVal;
						cy.log("set Date logging date offered out 0 returnedVal "+returnedVal)
						cy.log("set date logging date offered out 0 returnedDate "+returnedDate);

					});
				});

			});

		});
			cy.log("logging date offered out 0 +self.stText "+self.stText);
			cy.log("logging date offered out 0returnedDate "+returnedDate[0]);

	srmaAddPage.getAddCaseActionBtn().click();

	});


	it("User can click on a link to view a live SRMA record", function () {

		CaseManagementPage.getOpenActionsTable().children().should(($srma) => {
            expect($srma).to.be.visible
			expect($srma.text()).to.contain("SRMA");
        })

		CaseManagementPage.getOpenActionsTable().should(($status) => {
			expect($status).to.be.visible
			expect($status.text().trim()).to.contain(this.stText);
        })

		CaseManagementPage.getLiveSRMALink().click();
	});


	it("User on a live SRMA page can see a list of items", function () {

		//cy.get('[class="govuk-table__row"]').should(($row) => {
		srmaEditPage.getSrmaTableRow().should(($row) => {
			expect($row).to.have.length(7);
            expect($row.eq(0).text().trim()).to.contain(this.stText).and.to.match(/Status/i);
			expect($row.eq(1).text().trim()).to.contain(this.returnedDate).and.to.match(/(Date offered)/i);
			expect($row.eq(2).text().trim()).to.contain('Reason').and.to.match(/(Reason)/i);
			expect($row.eq(3).text().trim()).to.contain('Date accepted').and.to.match(/(Date accepted)/i);
			expect($row.eq(4).text().trim()).to.contain('Dates of visit').and.to.match(/(Dates of visit)/i);
			expect($row.eq(5).text().trim()).to.contain('Date report sent to trust').and.to.match(/(Date report sent to trust)/i);
			expect($row.eq(6).text().trim()).to.contain('Notes').and.to.match(/(Notes)/i);
	});
});


	/*
it("User on a live SRMA page can see conditional Edit/Add links", function () {


	//cy.get('[class="govuk-table__cell"]').each(($Cell, index) => {
	srmaEditPage.getSrmaTable().each(($Cell, index) => {
	//cy.log("cell count is"+$Cell.length);		
	srmaEditPage.getSrmaTableRow().eq(index).then(($row) => {

		if (srmaEditPage.checkForTableEntry() > 0 ) { 
			//cy.get('tr.govuk-table__row').eq(index).then(($row) => {
				
				//if (($Cell).find('.govuk-tag.ragtag.ragtag__grey').length > 0) {
				//if (($Cell).find(srmaEditPage.getSrmaTableRowEmpty() ).length > 0) {
					//cy.get('tr.govuk-table__row').eq(index).should('contain.text', 'Add');	
					srmaEditPage.getSrmaTableRow().eq(index).should('contain.text', 'Add');	
					cy.log("Empty");
					
				}else{
					srmaEditPage.getSrmaTableRow().eq(index).should('contain.text', 'Empty');	
					cy.log("Not Empty");
				}
			})
	});
});
*/

	it("User on a live SRMA page can see conditional Edit/Add links", function () {

			//cy.get('[class="govuk-table__cell"]').each(($Cell, index) => {
			//srmaEditPage.getSrmaTable().each(($Cell, index) => {
				srmaEditPage.getSrmaTableRow().each(($Trow, index) => {

					const t = $Trow.text();


			//cy.log("cell count is"+$Cell.length);		
			//srmaEditPage.getSrmaTableRow().eq(index).then(($row) => {
				cy.log("getSrmaTableRow index "+index)
				cy.log("$Trow.text "+$Trow.text() )
				cy.log("t "+t)
					//cy.get('tr.govuk-table__row').eq(index).then(($row) => {
						
						//if (($Cell).find('.govuk-tag.ragtag.ragtag__grey').length > 0) {
						if (t.includes("Empty")) {
							//cy.get('tr.govuk-table__row').eq(index).should('contain.text', 'Add');	
							srmaEditPage.getSrmaTableRow().eq(index).should('contain.text', 'Add');	
							cy.log("Empty");
							
						}else{
							srmaEditPage.getSrmaTableRow().eq(index).should('contain.text', 'Edit');	
							cy.log("Not Empty");
						}
					//})
			});
});



	it("User can Add/Edit SRMA Status", function () {

		//cy.get('[class="govuk-link"]').eq(0).click();
		srmaEditPage.getTableAddEditLink().eq(0).click();

		cy.log("setStatusSelect ").then(() => {
			cy.log(srmaAddPage.setStatusSelect("random") ).then((returnedVal) => { 
				cy.wrap(returnedVal.trim()).as("stText").then(() =>{
					stText  = returnedVal;
					cy.log(self.stText);

					cy.log(self.stText);
					cy.log("logging the result "+returnedVal)
					cy.log("logging the result "+self.stText)
					cy.log("logging the result "+stText)

					//cy.get('[id="add-srma-button"]').click();
					srmaAddPage.getAddCaseActionBtn().click();
					srmaEditPage.getSrmaTableRow().should(($row) => {
						expect($row.eq(0).text().trim()).to.contain(stText.trim()).and.to.match(/Status/i);
						});
	
				});
				cy.log(self.stText);
				stText  = returnedVal;
				cy.log("logging the result out 0 "+returnedVal)
				cy.log("logging the result out 0"+self.stText)
				cy.log("logging the result out 0 "+stText)


				});
				cy.log("logging the result out 1 "+self.stText)
				cy.log("logging the result out 1 "+self.stText)
				cy.log("logging the result out 1 "+stText)

			});
			cy.log("logging the result out 2 "+self.stText)
			cy.log("logging the result out 2 "+stText)
	
	});

	

		it("User can Add/Edit SRMA Date offered", function () {
			//cy.get('[class="govuk-link"]').eq(1).click();
			srmaEditPage.getTableAddEditLink().eq(1).click();


			cy.log("date offered closure ").then(() => {
				cy.log(srmaAddPage.setDateOffered() ).then((returnedVal) => { 
					cy.log("logging date offered closure inside nested "+returnedVal).then(() =>{
						cy.wrap(returnedVal.trim()).as("returnedDate").then(() =>{

						//returnedDate = returnedVal;
						cy.log("set Date logging date offered out 0 returnedVal "+returnedVal)
						cy.log("set date logging date offered out 0 returnedDate "+returnedDate);

					});
				});

			});

		});
			cy.log("logging date offered out 0 +self.stText "+self.stText);
			cy.log("logging date offered out 0returnedDate "+returnedDate[0]);

			//cy.get('[id="add-srma-button"]').click(); 
//			srmaAddPage.getAddCaseActionBtn().click();// We need to retrun to the page to handle value updates
//			srmaEditPage.getTableAddEditLink().eq(1).click();

///////////////------------


			cy.log("date offered closure ").then(() => {
				cy.log(srmaAddPage.getDateOffered() ).then((returnedVal) => { 
					cy.log("logging date offered closure inside nested "+returnedVal).then(() =>{
						cy.wrap(returnedVal.trim()).as("returnedDate").then(() =>{

						returnedDate = returnedVal;
						cy.log("set Date logging date offered out 0 returnedVal "+returnedVal)
						cy.log("set date logging date offered out 0 returnedDate "+returnedDate);

					});
				});

			});

			});

				srmaAddPage.getAddCaseActionBtn().click();
				srmaEditPage.getSrmaTableRow().should(($row) => {
					expect($row.eq(1).text().trim()).to.contain(returnedDate.trim()).and.to.match(/Date offered/i);
					})

	
			})


	it("User can Add/Edit SRMA Reason", function () {
		cy.get('[class="govuk-link"]').eq(2).click();
		cy.log("set Reason ").then(() => {
			cy.log(srmaAddPage.setReason("random") ).then((returnedVal) => { 
				cy.wrap(returnedVal.trim()).as("stText").then(() =>{
					stText  = returnedVal;
					cy.log(self.stText);

					cy.log(self.stText);
					cy.log("logging the result "+returnedVal)
					cy.log("logging the result "+self.stText)
					cy.log("logging the result "+stText)
					srmaAddPage.getAddCaseActionBtn().click();
						srmaEditPage.getSrmaTableRow().should(($row) => {
							expect($row.eq(2).text().trim()).to.contain(stText.trim()).and.to.match(/Reason/i);
						});
					});
				});
			});
		});



	it("User can Add/Edit SRMA Date accepted", function () {

		srmaEditPage.getTableAddEditLink().eq(3).click();
		cy.log("date offered closure ").then(() => {
			cy.log(srmaAddPage.setDateAccepted() ).then((returnedVal) => { 
				cy.log("logging date offered closure inside nested "+returnedVal).then(() =>{
					cy.wrap(returnedVal.trim()).as("returnedDate").then(() =>{

					returnedDate = returnedVal;
					cy.log("set Date logging date offered out 0 returnedVal "+returnedVal)
					cy.log("set date logging date offered out 0 returnedDate "+returnedDate);
				});
			});
		});
		srmaAddPage.getAddCaseActionBtn().click();
		srmaEditPage.getSrmaTableRow().should(($row) => {
			expect($row.eq(3).text().trim()).to.contain(returnedDate.trim()).and.to.match(/Date accepted/i);
			})
	});
		cy.log("logging date offered out 0 +self.stText "+self.stText);
		cy.log("logging date offered out 0returnedDate "+returnedDate[0]);
	});



	it("User can Add/Edit SRMA Date of visit", function () {

			//cy.get('[class="govuk-link"]').eq(4).click();
			srmaEditPage.getTableAddEditLink().eq(4).click();

			cy.log("dates of visit start closure ").then(() => {
				cy.log(srmaAddPage.setDateVisitStart() ).then((returnedVal) => { 
					cy.log("logging nested "+returnedVal).then(() =>{
						cy.wrap(returnedVal.trim()).as("returnedDate").then(() =>{
	
						returnedDate = returnedVal;
						cy.log("set Date logging date offered out 0 returnedVal "+returnedVal)
						cy.log("set date logging date offered out 0 returnedDate "+returnedDate);
					});
				});
			});
			srmaAddPage.getAddCaseActionBtn().click();
//			srmaEditPage.getSrmaTableRow().should(($row) => {
//				expect($row.eq(4).text().trim()).to.contain(returnedDate.trim()).and.to.match(/Dates of visit/i);
//				});




				const err = '[class="govuk-list govuk-error-summary__list"]';   
				cy.log((err).length);
		
					if ((err).length > 0 ) { 
		
						cy.get('[class="govuk-list govuk-error-summary__list"]').should('not.exist')
					}
	
				cy.get('[class="govuk-link"]').eq(4).click();
		});



	
			//Start date
	//		cy.get('[id="start-dtr-day"]').type(Math.floor(Math.random() * 21) + 10);
	//		cy.get('[id="start-dtr-month"]').type(Math.floor(Math.random() *3) + 10);
	//		cy.get('[id="start-dtr-year"]').type("2021");

	//		cy.get('[id="add-srma-button"]').click(); // We need to retrun to the page to handle value updates
	

			//Tests that there is no error validation to force entry of both dates

/*

			//End date
			cy.get('[id="end-dtr-day"]').type(Math.floor(Math.random() * 21) + 10);
			cy.get('[id="end-dtr-month"]').type(Math.floor(Math.random() *3) + 10);
			cy.get('[id="end-dtr-year"]').type("2023");

			// return to Page
			cy.get('[id="add-srma-button"]').click(); // We need to retrun to the page to handle value updates

			cy.get('[class="govuk-link"]').eq(4).click();
	
			//Validation of Start date
			cy.get('[id="start-dtr-day"]').invoke('val').then(dtrday => {
				cy.wrap(dtrday.trim()).as("day");
				arrDate[0] = dtrday;
			});
	
			cy.get('[id="start-dtr-month"]').invoke('val').then(dtrmon => {
				cy.wrap(dtrmon.trim()).as("month");
				arrDate[1] = dtrmon;
			});
	
			cy.get('[id="start-dtr-year"]').invoke('val').then(dtryr => {
				cy.wrap(dtryr.trim()).as("year");
				arrDate[2] = dtryr;
			});
						
			concatDate = (arrDate[0]+"-"+arrDate[1]+"-"+arrDate[2]);
			cy.log(concatDate);

			//Validation of End date
			cy.get('[id="end-dtr-day"]').invoke('val').then(dtrday => {
				cy.wrap(dtrday.trim()).as("day");
				arrDate[3] = dtrday;
			});
	
			cy.get('[id="end-dtr-month"]').invoke('val').then(dtrmon => {
				cy.wrap(dtrmon.trim()).as("month");
				arrDate[4] = dtrmon;
			});
	
			cy.get('[id="end-dtr-year"]').invoke('val').then(dtryr => {
				cy.wrap(dtryr.trim()).as("year");
				arrDate[5] = dtryr;
			});
						
			concatEndDate = (arrDate[3]+"-"+arrDate[4]+"-"+arrDate[5]);
			cy.log(concatEndDate);
			cy.get('[id="add-srma-button"]').click();

			//back on srma page validation
			cy.get('[class="govuk-table__row"]').should(($row) => {
				expect($row.eq(4).text().trim()).to.contain(concatDate.trim()).and.to.match(/Dates of visit/i);
			});

			cy.get('[class="govuk-table__row"]').should(($row) => {
				expect($row.eq(4).text().trim()).to.contain(concatEndDate.trim()); 
			});
*/
	});


	/*
	it("User can Add/Edit SRMA Date report sent to trust", function () {

		cy.get('[class="govuk-link"]').eq(5).click();
		cy.get('[id="dtr-day"]').type(Math.floor(Math.random() * 21) + 10);
		cy.get('[id="dtr-month"]').type(Math.floor(Math.random() *3) + 10);
		cy.get('[id="dtr-year"]').type("2021");
		cy.get('[id="add-srma-button"]').click();// We need to retrun to the page to handle value updates
		cy.get('[class="govuk-link"]').eq(5).click();
		cy.get('[id="dtr-day"]').invoke('val').then(dtrday => {
			cy.wrap(dtrday.trim()).as("day");
			arrDate[0] = dtrday;
		});

		cy.get('[id="dtr-month"]').invoke('val').then(dtrmon => {
			cy.wrap(dtrmon.trim()).as("month");
			arrDate[1] = dtrmon;
		});

		cy.get('[id="dtr-year"]').invoke('val').then(dtryr => {
			cy.wrap(dtryr.trim()).as("year");
			arrDate[2] = dtryr;
		});
					
		concatDate = (arrDate[0]+"-"+arrDate[1]+"-"+arrDate[2]);
		cy.log(concatDate);

			cy.get('[id="add-srma-button"]').click();
			cy.get('[class="govuk-table__row"]').should(($row) => {
				expect($row.eq(5).text().trim()).to.contain(concatDate.trim()).and.to.match(/Date report sent to trust/i);
			});

	});

	it("User can Add/Edit SRMA Notes", function () {

		let date = new Date();
		const lstring ='Cypress test run '+date;

		cy.get('[class="govuk-link"]').eq(6).click();
        cy.get('#srma-notes').should('be.visible');
        cy.get('#srma-notes-info').then(($inf) =>{         
        cy.get('#srma-notes').invoke('val', lstring); 
		})
		cy.get('#srma-notes').invoke('val').then(value => {
			term = value;
		});

		cy.get('[id="add-srma-button"]').click();
				cy.get('[class="govuk-table__row"]').should(($row) => {
					expect($row.eq(6).text().trim()).to.contain(term.trim()).and.to.match(/Notes/i);
				});
	
	});
*/	

	after(function () {
		cy.clearLocalStorage();
		cy.clearCookies();
	});

});
