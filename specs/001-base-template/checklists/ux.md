# Visual Design & UX Requirements Quality Checklist: Blazor Base Template

**Purpose**: Validate completeness, clarity, and measurability of visual design requirements (FR-020–FR-030, SC-011–SC-014) before implementation  
**Created**: 2026-04-16  
**Depth**: Standard  
**Audience**: Author (pre-implementation self-check)  
**Feature**: [spec.md](../spec.md) | [component-contracts.md](../contracts/component-contracts.md)

---

## Dark Mode & Theming

- [ ] CHK001 - Is the dark mode toggle placement explicitly specified (e.g., "icon button in MudAppBar, right-aligned")? [Clarity, Spec §FR-020]
- [ ] CHK002 - Are the exact icons for light and dark mode states defined (sun/moon, Material Filled.LightMode/DarkMode)? [Completeness, Spec §FR-020 + Contract §ThemeToggle]
- [ ] CHK003 - Is the initial dark mode state defined for first-time visitors (OS preference vs. default light)? [Gap, Spec §Assumptions]
- [ ] CHK004 - Are the MudTheme palette properties that MUST differ between Light and Dark explicitly listed (e.g., Primary, Surface, Background, DrawerBackground, AppbarBackground)? [Clarity, Spec §FR-021]
- [ ] CHK005 - Is the dark mode persistence mechanism specified beyond "localStorage or similar"? FR-020 says "SHOULD persist" — is this a firm requirement or optional? [Ambiguity, Spec §FR-020]
- [ ] CHK006 - Is the behavior defined when localStorage is unavailable or blocked (e.g., private browsing)? [Edge Case, Gap]
- [ ] CHK007 - Is the dark mode toggle latency quantified? SC-011 says "<100ms perceived delay" — is this testable, and what metric is used? [Measurability, Spec §SC-011]

## Typography & Font Loading

- [ ] CHK008 - Is the CDN URL for Inter font explicitly specified or left to implementation? If implementation-level, is there a requirement for preconnect hints? [Completeness, Spec §FR-023]
- [ ] CHK009 - Are font weight requirements specified (e.g., 400 regular, 500 medium, 600 semibold) or is just the font family sufficient? [Clarity, Spec §FR-023]
- [ ] CHK010 - Is the fallback behavior defined when the Inter font CDN is unreachable? Assumptions mention fallback stack, but is a requirement or acceptance scenario defined for it? [Coverage, Spec §Assumptions]
- [ ] CHK011 - Is the font loading strategy specified (font-display: swap vs. block vs. optional)? This affects perceived performance. [Gap]

## Glassmorphism & Surface Design

- [ ] CHK012 - Is "semi-transparent background" for the glassmorphism sidebar quantified (e.g., rgba opacity value or range)? [Clarity, Spec §FR-025]
- [ ] CHK013 - Are the glassmorphism requirements defined for BOTH light and dark modes with distinct values? [Completeness, Spec §FR-025 + Contract §CSS]
- [ ] CHK014 - Is the fallback behavior defined for browsers that don't support `backdrop-filter` (Safari <9, older browsers)? [Edge Case, Gap]
- [ ] CHK015 - Are the 3 tonal surface levels (FR-030) defined with specific color values or relationships (e.g., "each level differs by N% lightness")? [Measurability, Spec §FR-030]
- [ ] CHK016 - Is it specified which components use which surface level (e.g., cards on Surface, page on Background, drawer on DrawerBackground)? [Completeness, Spec §FR-030]

## Rounded Corners & Spacing

- [ ] CHK017 - Does FR-024 specify whether 12px applies uniformly to ALL components or allows exceptions (e.g., chips, avatars, full-round buttons)? [Clarity, Spec §FR-024]
- [ ] CHK018 - Is the MudBlazor `DefaultBorderRadius` override the ONLY mechanism, or are additional per-component radius overrides needed? [Consistency, Spec §FR-024 + Contract §Theme]

## Hover & Page Transitions

- [ ] CHK019 - Is the hover transition property specified beyond "all 150ms ease-in-out"? Using `transition: all` can cause unintended transitions — are specific properties defined (e.g., elevation, box-shadow, transform)? [Clarity, Spec §FR-026 + Contract §CSS]
- [ ] CHK020 - Is the MetricCard hover behavior fully specified? Contract says "elevation change" but spec says "subtle visual transition (e.g., elevation change or shadow shift)" — is the exact behavior pinned down? [Ambiguity, Spec §US2-Scenario 5 + Contract §MetricCard]
- [ ] CHK021 - Is the page transition type explicitly chosen (fade vs. slide-in)? FR-027 says "fade or slide-in" — this "or" leaves ambiguity for implementation. [Ambiguity, Spec §FR-027]
- [ ] CHK022 - Is the page transition duration specified? Contract defines `@keyframes fadeIn` but no duration value is given in the spec. [Gap, Spec §FR-027]
- [ ] CHK023 - Are page transition requirements defined for BOTH entering and leaving routes, or only entering? [Completeness, Spec §FR-027]

## Skeleton Loading & Loading States

- [ ] CHK024 - Is it specified WHICH components use skeleton loading vs. spinner? FR-028 says "where feasible" — are the specific components enumerated? [Clarity, Spec §FR-028]
- [ ] CHK025 - Is the skeleton shimmer animation duration/style defined or left to MudBlazor defaults? [Gap, Spec §FR-028]
- [ ] CHK026 - Are skeleton loading requirements consistent between the LoadingPlaceholder component (3 types: Spinner, Skeleton, Linear) and the DataTable component (MudSkeleton only)? [Consistency, Contract §LoadingPlaceholder + Contract §DataTable]

## Accessibility & Reduced Motion

- [ ] CHK027 - Does FR-029 define what constitutes an "essential state change" that is exempt from the prefers-reduced-motion override? [Clarity, Spec §FR-029]
- [ ] CHK028 - Is the prefers-reduced-motion CSS override strategy consistent with SC-014 ("no animations or transitions beyond essential state changes")? The CSS contract sets duration to `0.01ms` — does this match "no animations"? [Consistency, Spec §FR-029 + Contract §CSS + Spec §SC-014]
- [ ] CHK029 - Are WCAG AA contrast requirements specified with specific color hex values or only as a ratio constraint (≥4.5:1)? Without specific colors, how will implementors verify compliance? [Measurability, Spec §FR-022]
- [ ] CHK030 - Is keyboard navigation defined for the ThemeToggle button (Tab focus, Enter/Space activation)? [Coverage, Gap]
- [ ] CHK031 - Are focus indicator styles specified for interactive elements, or are MudBlazor defaults assumed sufficient? [Gap]

## Cross-Cutting & Consistency

- [ ] CHK032 - Are dark mode visual requirements defined for ALL 8 components, or only MainLayout and MetricCard (US1-Scenario 8, US2-Scenario 6)? What about DataTable, LoadingPlaceholder, WelcomeSection, AppLogo in dark mode? [Coverage, Gap]
- [ ] CHK033 - Is the glassmorphism effect on the sidebar consistent with the 3 tonal surface requirement? Does DrawerBackground count as one of the 3 levels, and how does glassmorphism transparency interact with it? [Consistency, Spec §FR-025 + §FR-030]
- [ ] CHK034 - Are hover transition requirements defined for NavMenu links in addition to cards and buttons? The CSS contract includes `.mud-nav-link` but the spec only mentions "buttons, cards, links" generically. [Consistency, Spec §FR-026 + Contract §CSS]

---

## Notes

- Check items off as completed: `[x]`
- Add findings or resolution notes inline after each item
- Items reference spec sections `[Spec §FR-XXX]`, contracts `[Contract §Section]`, or mark gaps `[Gap]`
- 34 total items across 8 categories covering FR-020–FR-030, SC-011–SC-014, and visual design contracts
