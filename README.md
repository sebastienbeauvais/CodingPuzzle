
Given the string "(id, name, email, type(id, name, customFields(c1, c2, c3)), externalId)" parse to give the output
- id
- name
- email
- type
 - id
 - name
 - customFields
  - c1
  - c2
  - c3
- externalId

AND this output

- externalId
- email
- id
- name
- type
 - customFields
  - c1
  - c2
  - c3
 - id
 - name
    
NOTE: each bullet point should just be a standard '-'
