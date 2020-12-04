'use strict'
const merge = require('webpack-merge')
const prodEnv = require('./prod.env')

module.exports = merge(prodEnv, {
  NODE_ENV: '"development"',
  AuthHost: '"http://localhost:3333"', //身份认证地址
  ApiHost: '"http://localhost:4444"', //api地址
})
