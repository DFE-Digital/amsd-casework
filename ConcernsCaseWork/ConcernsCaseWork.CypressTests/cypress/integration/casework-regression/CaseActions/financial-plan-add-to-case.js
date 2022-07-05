//import utils from "../../../support/utils";
import utils from "/cypress/support/utils"
import AddToCasePage from "/cypress/pages/caseActions/addToCasePage";
import FinancialPlanPage from "/cypress/pages/caseActions/financialPlanPage";
import CaseManagementPage from "/cypress/pages/caseMangementPage";

describe("User can add Financial Plan case action to an existing case", () => {
	before(() => {
		cy.login();
	});

	afterEach(() => {
		cy.storeSessionData();
	});

	const searchTerm ="Accrington St Christopher's Church Of England High School";
	let term = "";
	let stText = "";
	let newString  = "";
	let $status = "";
	let concatDate = "null" //["date1", "date2"];
	let concatEndDate = "";
	let arrDate = ["day1", "month1", "year1","day2", "month2", "year2", ];
	let dpreq = "";
	let dprec = "";

	it("User enters the case page", () => {
		cy.checkForExistingCase();
	});

	it("User has option to select a Financial Plan Case Action", () => {
		cy.reload();
		CaseManagementPage.getAddToCaseBtn().click();
		AddToCasePage.addToCase('FinancialPlan')
		AddToCasePage.getCaseActionRadio('FinancialPlan').siblings().should('contain.text', AddToCasePage.actionOptions[2]);

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
					AddToCasePage.addToCase('FinancialPlan');
					AddToCasePage.getCaseActionRadio('FinancialPlan').siblings().should('contain.text', AddToCasePage.actionOptions[2]);
					AddToCasePage.getAddToCaseBtn().click();

		}else {
			cy.log("No Case Action exists");	
			cy.log(utils.checkForGovErrorSummaryList() );


		}

		/*
			const err = Cypress.$(utils.getGovErrorSummaryList() ); 
			cy.log(err.length);

			if (err.length > 0) { //Checks for existing case action
				
				cy.log("Case Action already exists");

					cy.visit(Cypress.env('url'), { timeout: 30000 });
					cy.checkForExistingCase(true);
					CaseManagementPage.getAddToCaseBtn().click();
					AddToCasePage.addToCase('FinancialPlan');
					AddToCasePage.getCaseActionRadio('FinancialPlan').siblings().should('contain.text', AddToCasePage.actionOptions[2]);
					AddToCasePage.getAddToCaseBtn().click();	
				}else{
					cy.log("No Case Action exists");	
				}

*/
			});



			it("User is taken to the correct Case Action page", () => {
				FinancialPlanPage.getHeadingText().then(term => {
					expect(term.text().trim()).to.match(/(Financial Plan)/i);
				});
		});


	it("User is shown error when entering an invalid date", () => {

		FinancialPlanPage.getDatePlanRequestedDay().type(Math.floor(Math.random() * 21) + 10);
		FinancialPlanPage.getDatePlanRequestedMon().type(Math.floor(Math.random() *3) + 10);
		//FinancialPlanPage.getDatePlanRequestedYear().type("2022");
		FinancialPlanPage.getUpdateBtn().click();
		utils.validateGovErorrList('invalid date');
		cy.reload();

	});

	it("User can select a Financial Plan status", function () {

		FinancialPlanPage.statusSelect().then((returnedVal) => {
			cy.wrap(returnedVal.trim()).as("stText");

			//newString = returnedVal

			//cy.log("inside "+newString);
			//cy.log("inside "+pleaseWork);
			//cy.log("inside "+stText);
			//cy.log("inside "+this.stText);
			//cy.log("inside "+self.stText);
		});

	});

	it("User cannot progress with more than the max character limit in the Notes section", function () {
		const lstring =
        'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwx';
		let date = new Date();

		FinancialPlanPage.getNotesBox().should('be.visible');

		FinancialPlanPage.getNotesInfo().then(($inf) =>{
            expect($inf).to.be.visible
            expect($inf.text()).to.match(/(2000 characters remaining)/i)   

            let text = Cypress._.repeat(lstring, 40)
            expect(text).to.have.length(2000);
  
			FinancialPlanPage.getNotesBox().invoke('val', text);
			FinancialPlanPage.getNotesBox().type('{shift}{alt}'+ "   Staging test");

		});

		FinancialPlanPage.getUpdateBtn().click();
		utils.validateGovErorrList('Notes must be 2000 characters or less');

		cy.reload(true);
		FinancialPlanPage.getNotesBox().invoke('val',  " ");
		FinancialPlanPage.getNotesBox().type('{shift}{alt}'+ "Staging test \n" + date);
		
		//FinancialPlanPage.statusSelect()

		FinancialPlanPage.statusSelect().then((returnedStatus) => {
			cy.wrap(returnedStatus.trim()).as("stText");

			newString = returnedStatus
			//cy.log("inside "+newString);
			cy.log("inside "+returnedStatus);
			//cy.log("inside "+stText);
			//cy.log("inside "+this.stText);
			//cy.log("inside "+self.stText);
		});

		//cy.log("logging the result  "+FinancialPlanPage.setDatePlanRequested() )


	});




		it("User can set a valid date plan was requested", function () {
			
			cy.log("logging the result requested then ").then(() => {
			cy.log(FinancialPlanPage.setDatePlanRequested() ).then((returnedVal) => { 
				cy.log("logging the result inside nested val date test "+returnedVal)
				});
			});
	});

		it("User can set a valid date plan was received", function () {
				
			cy.log("logging the result requested then ").then(() => {
			cy.log(FinancialPlanPage.setDatePlanReceived() ).then((returnedVal) => { 
				cy.log("logging the result inside nested val date test "+returnedVal)
				});
			});
	});


	it("User can successfully add a Financial Plan case action to a case", () => {
		FinancialPlanPage.getUpdateBtn().click();
	});

	it("User can view a live Financial Plan record in the table", function () {

			CaseManagementPage.getOpenActionsTable().then(($openAction) => {
            expect($openAction).to.be.visible;

			expect($openAction).to.contain('Financial Plan');
			cy.log(this.stText)

			expect($openAction).to.contain(this.stText);
        })
	});


	it("User can click on a link to view a live Financial Plan record", function () {

		cy.get('a[href*="/action/financialplan/"]').click();
	});

	it("User on a live Financial Plan page can see a list of items", function () {

		FinancialPlanPage.getItemsTable().then(($row) => {
			expect($row).to.have.length(4);
            expect($row.eq(0).text().trim()).to.contain(this.stText).and.to.match(/Status/i);
			expect($row.eq(1).text().trim()).to.contain(dpreq).and.to.match(/(Date plan requested)/i);
			expect($row.eq(2).text().trim()).to.contain(dprec).and.to.match(/(Date viable plan received)/i);
			expect($row.eq(3).text().trim()).to.contain('Notes');//.and.to.match(/(Notes)/i);
	});
});


	it("User on a live Financial Plan page can see conditional Edit/Add links", function () {

			cy.get('[class="govuk-table__cell"]').each(($Cell, index) => {
			cy.log("cell count is"+$Cell.length);		
					cy.get('tr.govuk-table__row').eq(index).then(($row) => {
						
						if (($Cell).find('.govuk-tag.ragtag.ragtag__grey').length > 0) {
							cy.get('tr.govuk-table__row').eq(index).should('contain.text', 'Add');	
							cy.log("Empty");
							
						}else{
							cy.get('tr.govuk-table__row').eq(index).should('contain.text', 'Edit');	
							cy.log("Not Empty");
						}
					})
			});
});

/*
	it("User can Add/Edit SRMA Status", function () {

		cy.get('[class="govuk-link"]').eq(0).click();

		let rand = Math.floor(Math.random()*2)

		cy.get('[id*="status"]').eq(rand).click();
		cy.get('label.govuk-label.govuk-radios__label').eq(rand).invoke('text').then(term => {
			cy.wrap(term.trim()).as("stText");
			cy.log("Status set as "+term);
			cy.get('[id="add-srma-button"]').click();
			cy.get('[class="govuk-table__row"]').should(($row) => {
				expect($row.eq(0).text().trim()).to.contain(term.trim()).and.to.match(/Status/i);
			});
		})

	});

		it("User can Add/Edit SRMA Date offered", function () {
			cy.get('[class="govuk-link"]').eq(1).click();
			cy.get('[id="dtr-day"]').type(Math.floor(Math.random() * 21) + 10);
			cy.get('[id="dtr-month"]').type(Math.floor(Math.random() *3) + 10);
			cy.get('[id="dtr-year"]').type("2021");	
			cy.get('[id="add-srma-button"]').click();// We need to retrun to the page to handle value updates
			cy.get('[class="govuk-link"]').eq(1).click();
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
					expect($row.eq(1).text().trim()).to.contain(concatDate.trim()).and.to.match(/Date offered/i);
				});
			})

	it("User can Add/Edit SRMA Reason", function () {

		cy.get('[class="govuk-link"]').eq(2).click();

		let rand = Math.floor(Math.random()*2)

		cy.get('[id^="reason"]').eq(rand).click();
		cy.get('label.govuk-label.govuk-radios__label').eq(rand).invoke('text').then(term => {
			cy.wrap(term.trim()).as("stText");
			cy.log("Reason set as "+term);
			cy.get('[id="add-srma-button"]').click();
			cy.get('[class="govuk-table__row"]').should(($row) => {
				expect($row.eq(2).text().trim()).to.contain(term.trim()).and.to.match(/Reason/i);
			});
		})
	});

	it("User can Add/Edit SRMA Date accepted", function () {

		cy.get('[class="govuk-link"]').eq(3).click();
		cy.get('[id="dtr-day"]').type(Math.floor(Math.random() * 21) + 10);
		cy.get('[id="dtr-month"]').type(Math.floor(Math.random() *3) + 10);
		cy.get('[id="dtr-year"]').type("2021");
		cy.get('[id="add-srma-button"]').click();// We need to retrun to the page to handle value updates
		cy.get('[class="govuk-link"]').eq(3).click();

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
				expect($row.eq(3).text().trim()).to.contain(concatDate.trim()).and.to.match(/Date accepted/i);
			});
	});


	it("User can Add/Edit SRMA Date of visit", function () {

			cy.get('[class="govuk-link"]').eq(4).click();
	
			//Start date
			cy.get('[id="start-dtr-day"]').type(Math.floor(Math.random() * 21) + 10);
			cy.get('[id="start-dtr-month"]').type(Math.floor(Math.random() *3) + 10);
			cy.get('[id="start-dtr-year"]').type("2021");

			cy.get('[id="add-srma-button"]').click(); // We need to retrun to the page to handle value updates
	

			//Tests that there is no error validation to force entry of both dates
			const err = '[class="govuk-list govuk-error-summary__list"]';   
			cy.log((err).length);
	
				if ((err).length > 0 ) { 
	
					cy.get('[class="govuk-list govuk-error-summary__list"]').should('not.exist')
				}

			cy.get('[class="govuk-link"]').eq(4).click();


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
	});

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
