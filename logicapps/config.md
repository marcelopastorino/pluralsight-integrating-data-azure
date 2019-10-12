
# LogicApp

```json
{
   "properties": {
   "DataFactoryName": {
   "type": "string"
   },
   "EmailTo": {
   "type": "string"
   },
   "ErrorMessage": {
   "type": "string"
   },
   "PipelineName": {
   "type": "string"
   },
   "Subject": {
   "type": "string"
   }
   },
   "type": "object"
}
```

# Web activity

```json
{   
   "DataFactoryName": "@{pipeline().DataFactory}",
   "PipelineName": "@{pipeline().Pipeline}",
   "Subject": "Successful pipeline execution.",
   "ErrorMessage": "No errors.",
   "EmailTo": "hello@softwaredeveloper.io"
}

{
   "DataFactoryName": "@{pipeline().DataFactory}",
   "PipelineName": "@{pipeline().Pipeline}",
   "Subject": "Pipeline execution failed.",
   "ErrorMessage": "Please see logs for more information.",
   "EmailTo": "hello@softwaredeveloper.io"
}
```


