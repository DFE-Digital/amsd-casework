## =============================================================================
#  Configure the AWS Provider                                                  #
## =============================================================================

# Provides the AWS account ID to other resources
# Interpolate: data.aws_caller_identity.current.account_id
data "aws_caller_identity" "current" {}

terraform {
  required_providers {
	aws = "~> 2.59"
	cloudfoundry = {
	  source  = "cloudfoundry-community/cloudfoundry"
	  version = ">= 0.12.6"
	}
  }
  ## ========================================================================== ##
  #  Terraform State S3 Bucket                                                   #
  ## ========================================================================== ##
  backend s3 {
	key    	= local.aws_service_key
	encrypt = true
  }
}

provider aws {}

provider cloudfoundry {
  api_url	= var.cf_api_url
  user      = var.cf_user
  password  = var.cf_password
}
