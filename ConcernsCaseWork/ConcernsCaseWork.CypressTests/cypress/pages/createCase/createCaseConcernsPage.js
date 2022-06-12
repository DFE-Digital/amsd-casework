class CreateCaseConcernsPage {

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

    selectConcern(expectedNumberOfRagStatus, ragStatus){

        switch (ragStatus) {
            case "Red plus":
                cy.get(
                    ".govuk-table__body .govuk-table__row .govuk-ragtag-wrapper .ragtag__redplus"
                ).should("have.length", expectedNumberOfRagStatus);
                break;
            case "Amber":
                cy.get(
                    ".govuk-table__body .govuk-table__row .govuk-ragtag-wrapper .ragtag__amber"
                ).should("have.length", expectedNumberOfRagStatus);
                break;
            case "Green":
                cy.get(
                    ".govuk-table__body .govuk-table__row .govuk-ragtag-wrapper .ragtag__green"
                ).should("have.length", expectedNumberOfRagStatus);
                break;
            case "Red":
                cy.get(
                    ".govuk-table__body .govuk-table__row .govuk-ragtag-wrapper .ragtag__red"
                ).should("have.length", expectedNumberOfRagStatus);
                break;
            default:
                cy.log("Could not do the thing");
        }
        return expectedNumberOfRagStatus;
    }

    selectConcernType(){
        cy.get(".govuk-radios__item [value=Financial]").click();
        cy.get("[id=sub-type-3]").click();
        cy.get("[id=rating-3]").click();
        cy.get(".govuk-button").click();
    }
}
    

    export default new CreateCaseConcernsPage();