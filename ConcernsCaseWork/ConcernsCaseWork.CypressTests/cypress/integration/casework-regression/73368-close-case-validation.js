
describe("Users can see warning messages on the case closure page", () => {
	before(() => {
		cy.login();
	});

	afterEach(() => {
		cy.storeSessionData();
	});

	const searchTerm =
		"Accrington St Christopher's Church Of England High School";

	const lstring =
		'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwx';

	//Opens the first active case in the list
	it("Opens an active case", () => {
		cy.visit('https://amsd-casework-dev.london.cloudapps.digital')
		//Storing case id string - not used
		cy.get('#your-casework tr:nth-child(1)  td:nth-child(1)  a').then(($el) => {
			cy.wrap($el.text()).as("closedCaseId");

		});

		cy.get('#your-casework tr:nth-child(1) td:nth-child(1) a').click();
	});

	it("User can close any open concerns", () => {
			const elem = '.govuk-table-case-details__cell_no_border [href*="edit_rating"]';
			if (Cypress.$(elem).length > 0) { //Cypress.$ needed to handle element missing exception

				cy.get('.govuk-table-case-details__cell_no_border [href*="edit_rating"]').its('length').then(($elLen) => {
					cy.log($elLen)

				while ($elLen > 0) {
						cy.get('.govuk-table-case-details__cell_no_border [href*="edit_rating"]').eq($elLen-1).click();
						cy.get('[href*="closure"]').click();
						cy.get('.govuk-button-group [href*="edit_rating/closure"]:nth-of-type(1)').click();
						$elLen = $elLen-1
						cy.log($elLen+" more open concerns")				
						}
					});
			}else {
				cy.log('All concerns closed')
			}

		});

	it("User can enter case closure page", () => {
		cy.get('#close-case-button').click()
	})

	
	it("User can type 200 characters max into the outcome box", function () {
		cy.get("#case-outcomes-info").then(($info) =>{
            expect($info).to.be.visible
            expect($info.text()).to.match(/(200 characters remaining)/i)   

            let text = Cypress._.repeat(lstring, 4)
            expect(text).to.have.length(200);
            
        cy.get('#case-outcomes').invoke('val', text);
        cy.get('#case-outcomes').type('{shift}{alt}'+ '1');
	})
});

	it("User can see the character count reduce accordingly", () => {
        cy.get("#case-outcomes-info").then(($info2) =>{
            expect($info2).to.be.visible
            expect($info2.text()).to.match(/(1 character too many)/i);      
            })
        })

	it("User can see the text box expand when typing large numbers", () => {
		cy.get('#case-outcomes').should(($box) => {
			expect($box).to.not.have.attr('style', 'height: 113px; border-color: black;')
			expect($box).to.have.attr('style', 'height: 139px; border-color: green;')
			})
        })

	it("User closing a case with outcome exceeding max characters is shown error", () => {
	    cy.get('#close-case-button').click();
	    cy.get('#errorSummary').children().then(($error) =>{
		   expect($error).to.be.visible
		   expect($error.text()).to.match(/(too many characters)/i);
		   cy.get('#case-outcomes').scrollIntoView().clear().wait(1000);
		})

	})

//	Reload required due to Bug Number 88567
	it("User closing a case with no outcome text is shown an error", () => {
		cy.reload()
	    cy.get('#close-case-button').click();
	    cy.get('#errorSummary').children().then(($error) =>{
		   expect($error).to.be.visible
		   expect($error.text()).to.match(/(not recorded an outcome)/i);
		})

	})
})

	after(function () {
		cy.clearLocalStorage();
		cy.clearCookies();
	});
