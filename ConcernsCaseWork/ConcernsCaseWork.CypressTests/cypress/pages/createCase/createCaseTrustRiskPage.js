class CreateCaseTrustRiskPage {

    constructor() {
        const concernsRgx = new RegExp(/(Compliance|Financial|Force majeure|Governance|Irregularity)/, 'i');
        const trustRgx = new RegExp(/(School|Academy|Accrington|South|North|East|West)/, 'i');
        const ragRgx = new RegExp(/(amber|green|red|redPlus|Red Plus)/, 'i');
        const dotRgx = new RegExp(/(Deteriorating|Unchanged|Improving)/, 'i');

    }

    //locators
    getHeading() {
        return cy.get("#ADD HEADING ELEMENT HERE");
    }
    
    //methods


    selectTrustRisk(){
        cy.get('[href="/case/rating"]').click();
        cy.get(".ragtag").should("be.visible");
        //Randomly select a RAG status
        cy.get(".govuk-radios .ragtag:nth-of-type(1)")
            .its("length")
            .then((ragtagElements) => {
                let num = Math.floor(Math.random() * ragtagElements);
                cy.get(".govuk-radios .ragtag:nth-of-type(1)").eq(num).click();
            });
        cy.get("#case-rating-form > div.govuk-button-group > button").click();

    }


}
    

    export default new CreateCaseTrustRiskPage();