class Utils {

    constructor() {
        //this.something = 
        let arrDate = ["day1", "month1", "year1","day2", "month2", "year2", ];
        //const $elem = Cypress.$('.govuk-list.govuk-error-summary__list');
    }

    //locators
    getGovErrorSummaryList() {
        return     cy.get('.govuk-list.govuk-error-summary__list');
    }

    getGovErrorContainer() {
        return     cy.get('[id="errorSummary"]');
    }

    //Methods

    /*Takes a string arg of the error test to be validated
    */
    validateGovErorrList(textToVerify) {
        this.getGovErrorSummaryList().should('contain.text', textToVerify);
	}

    //Checks is the error summary list exists, and returns it's leng as a integer
    //
    checkForGovErrorSummaryList() {

        //let $elem = Cypress.$('.govuk-list.govuk-error-summary__list');
        let $elem = Cypress.$('[id="errorSummary"]');
        cy.log(($elem).length)
        
        return ($elem.length);
    }

        
}
    
    export default new Utils();
