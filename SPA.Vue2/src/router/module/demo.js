/**
 * demo
 */
export default [{
    path: '/demo',
    name: 'Demo',
    component: () => import('@/view/Demo')
  },
  {
    path: '/test',
    name: 'Test',
    component: () => import('@/view/Test')
  },
  {
    path: '/user',
    name: 'User',
    component: () => import('@/view/User')
  }
]
