//package com.ecoekb.ui.composeble
//
//import androidx.compose.foundation.layout.Spacer
//import androidx.compose.foundation.layout.width
//import androidx.compose.material3.Icon
//import androidx.compose.material3.Text
//import androidx.compose.runtime.Composable
//import androidx.compose.runtime.getValue
//import androidx.compose.ui.Modifier
//import androidx.compose.ui.unit.dp
//import androidx.navigation.NavController
//import androidx.navigation.compose.currentBackStackEntryAsState
//import com.ecoekb.ui.navigation.Screens
//import androidx.compose.material.BottomNavigation
//import androidx.compose.material.BottomNavigationItem
//import androidx.navigation.compose.rememberNavController

//
//val items = listOf(
//    Screens.StockCategories,
//    Screens.Orders,
//    Screens.Objects,
//    Screens.Settings
//)
//
//val noBarScreen = listOf(
//    Screens.Login,
//    Screens.Registration,
//    Screens.OnBoarding
//)
//
//@Composable
//fun NavigationBar(modifier: Modifier = Modifier, navController: NavController) {
//
//    val navBackStackEntry by navController.currentBackStackEntryAsState()
//    val currentDestination = navBackStackEntry?.destination
//    if (currentDestination?.route !in noBarScreen.map { item -> item.route }) {
//
//        NavigationBar(
//            modifier = modifier,
//            navController = rememberNavController()
//        )
//
//        BottomNavigation(
//            modifier = modifier,
//        ) {
//            val navBackStackEntry by navController.currentBackStackEntryAsState()
//            val currentDestination = navBackStackEntry?.destination
//            items.forEach { screen: Screens ->
//                BottomNavigationItem(
//                    icon = {
//                        Icon(
//                            imageVector = screen.image!!,
//                            contentDescription = null
//                        )
//                    },
//                    label = {
//                        Text(stringResource(screen.resourceId!!))
//                    },
//                    selected = currentDestination?.hierarchy?.any { it.route == screen.route } == true,
//                    onClick = {
//                        navController.navigate(screen.route) {
////                            popUpTo(navController.graph.findStartDestination().id) {
////                                saveState = true
////                            }
//                            launchSingleTop = true
//                            restoreState = true
//                        }
//                    }
//                )
//            }
//        }
//    } else {
//        Spacer(modifier = Modifier.width(0.dp))
//    }
//}