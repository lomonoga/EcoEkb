package com.ecoekb.ui.screens.onboarding

import androidx.compose.animation.core.Spring
import androidx.compose.animation.core.animateDpAsState
import androidx.compose.animation.core.spring
import androidx.compose.foundation.ExperimentalFoundationApi
import androidx.compose.foundation.Image
import androidx.compose.foundation.background
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.pager.HorizontalPager
import androidx.compose.foundation.pager.rememberPagerState
import androidx.compose.foundation.shape.CircleShape
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.material.*
import androidx.compose.material3.FloatingActionButton
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.material3.TextButton
import androidx.compose.runtime.Composable
import androidx.compose.runtime.rememberCoroutineScope
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.draw.clip
import androidx.compose.ui.graphics.ImageBitmap
import androidx.compose.ui.layout.ContentScale
import androidx.compose.ui.platform.LocalContext
import androidx.compose.ui.res.imageResource
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import androidx.navigation.NavController
import com.ecoekb.ui.navigation.Screens


import kotlinx.coroutines.launch

@OptIn(ExperimentalFoundationApi::class)
@Composable
fun OnBoarding(navController: NavController) {
    val onboardingDataStore = OnboardingDataStore(LocalContext.current)

    val scope = rememberCoroutineScope()

    Column(Modifier.fillMaxSize()) {

        val items = OnBoardingItem.get()
        val statePager = rememberPagerState()

        TopSection(
            size = items.size,
            index = statePager.currentPage,
            navController = navController,
            onboardingDataStore = onboardingDataStore
        )

        HorizontalPager(
            state = statePager,
            modifier = Modifier
                .fillMaxSize()
                .weight(0.8f),
            pageCount = items.size
        ) { page ->
            OnBoardingItem(items[page])
        }

        Row(
            modifier = Modifier.fillMaxWidth(),
            horizontalArrangement = Arrangement.SpaceBetween
        ) {
            BottomSection(
                size = items.size,
                index = statePager.currentPage
            ) {
                if (statePager.currentPage + 1 < items.size) {
                    scope.launch {
                        statePager.animateScrollToPage(statePager.currentPage + 1)
                    }
                } else {
                    scope.launch {
                        onboardingDataStore.saveOnboardingShow(false)
                    }
                    navController.navigate(Screens.Login.route)
                }
            }
        }
    }
}

@Composable
fun TopSection(
    size: Int,
    index: Int,
    navController: NavController,
    onboardingDataStore: OnboardingDataStore,
) {
    val scope = rememberCoroutineScope()

    Box(
        modifier = Modifier
            .fillMaxWidth()
            .padding(16.dp)
    ) {

        Indicators(size, index)

        TextButton(
            onClick = {
                scope.launch {
                    onboardingDataStore.saveOnboardingShow(false)
                }
                navController.navigate(Screens.Login.route)
                },
            modifier = Modifier.align(Alignment.CenterEnd)
        ) {
            Text(
                text = "Пропустить",
                color = MaterialTheme.colorScheme.onBackground,
                style = MaterialTheme.typography.bodyMedium
            )
        }
    }
}

@Composable

fun BottomSection(
    size: Int,
    index: Int,
    onNextClicked: () -> Unit
) {
    Box(
        modifier = Modifier
            .padding(16.dp)
    ) {
        val buttontext = if (size == index + 1) "Я с вами!" else "Продолжить"

        FloatingActionButton(
            onClick = onNextClicked,
            shape = RoundedCornerShape(5.dp),
            modifier = Modifier.fillMaxWidth(),
            containerColor = MaterialTheme.colorScheme.primary,
            contentColor = MaterialTheme.colorScheme.onPrimary
        ) {
            Text(
                text = buttontext,
                style = MaterialTheme.typography.bodyMedium,
                color = MaterialTheme.colorScheme.onPrimary
            )
        }
    }
}

@Composable
fun BoxScope.Indicators(size: Int, index: Int) {
    Row(
        verticalAlignment = Alignment.CenterVertically,
        horizontalArrangement = Arrangement.spacedBy(16.dp),
        modifier = Modifier.align(Alignment.CenterStart)
    ) {
        repeat(size) {
            Indicator(isSelected = it == index)
        }
    }
}

@Composable
fun Indicator(isSelected: Boolean) {
    val width = animateDpAsState(
        targetValue = if (isSelected) 25.dp else 10.dp,
        animationSpec = spring(dampingRatio = Spring.DampingRatioMediumBouncy),
        label = ""
    )

    Box(
        modifier = Modifier
            .height(10.dp)
            .width(width.value)
            .clip(CircleShape)
            .background(
                color = if (isSelected) MaterialTheme.colorScheme.primary else MaterialTheme.colorScheme.secondary
            )
    ) {

    }
}

@Composable

fun OnBoardingItem(item: OnBoardingItem) {
    Column(
        horizontalAlignment = Alignment.CenterHorizontally,
        verticalArrangement = Arrangement.Center,
        modifier = Modifier.fillMaxSize().padding(16.dp)
    ) {
        Image(
            bitmap = ImageBitmap.imageResource(id = item.image),
            contentDescription = "Screen1",
            modifier = Modifier.fillMaxWidth(),
            contentScale = ContentScale.FillWidth
            )

        Text(
            text = item.title,
            style = MaterialTheme.typography.titleMedium,
            color = MaterialTheme.colorScheme.onBackground,
            textAlign = TextAlign.Center
        )

        Text(
            text = item.text,
            style = MaterialTheme.typography.bodyMedium,
            color = MaterialTheme.colorScheme.secondary,
            textAlign = TextAlign.Center
        )
    }
}