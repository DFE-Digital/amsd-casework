class CaseManagementPage {

    constructor() {
        //this.arrDate = ["day1", "month1", "year1","day2", "month2", "year2", ];
        //this.$elem = Cypress.$('.govuk-table-case-details__cell_no_border [href*="edit_rating"]');
        //const $elem = Cypress.$('.govuk-table-case-details__cell_no_border [href*="edit_rating"]');
    }

    //locators
    getHeadingText() {
        return cy.get('h1[class="govuk-heading-l"]');
    }

    getSubHeadingText() {
        return cy.get('[class="govuk-caption-m"]');
    }

    getTrustHeadingText() {
        return cy.get('h2[class="govuk-heading-m"]');
    }

    getConcernEditBtn() {
        return cy.get('.govuk-table-case-details__cell_no_border [href*="edit_rating"]');
    }

    getAddToCaseBtn() {
        return cy.get('[class="govuk-button"][role="button"]').contains('Add to case');
    }

    getCaseDetailsTab() {
        return cy.get('[id="tab_case-details"]');
    }

    getTrustOverviewTab() {
        return cy.get('[id="tab_trust-overview"]');
    }

    getOpenActionsTable() {
        return cy.get('[id="open-case-actions"]');
    }

    getClosedActionsTable() {
        return cy.get('[id="closed-case-actions"]');
    }

    getCloseCaseBtn() {
        return cy.get('#close-case-button');
    }

    getLiveSRMALink() {
        return cy.get('a[href*="/action/srma/"]');//.contains('srma');
    }    
   

    //methods

   closeAllOpenConcerns() {

    const $elem = Cypress.$('.govuk-table-case-details__cell_no_border [href*="edit_rating"]');

    cy.log("About to close all lopen concerns");
    cy.log("$elem.length ="+($elem).length)

        
    if (($elem).length > 0 ) { //Cypress.$ needed to handle element missing exception

        this.getConcernEditBtn().its('length').then(($elLen) => {
            cy.log("Method $elLen " +$elLen)
    
        while ($elLen > 0) {

                this.getConcernEditBtn().eq($elLen-1).click();
                cy.get('[href*="closure"]').click();
                cy.get('.govuk-button-group [href*="edit_rating/closure"]:nth-of-type(1)').click();
                $elLen = $elLen-1
                cy.log($elLen+" more open concerns")				
                }
            });
    }else {
        cy.log('All concerns closed')
        }
    
    }


    checkForOpenActions() {

        const $elem = Cypress.$('[id="open-case-actions"]');
        cy.log(($elem).length)
        
        return ($elem.length);
    }

   



}

    
    export default new CaseManagementPage();